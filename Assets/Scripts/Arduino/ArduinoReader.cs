using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;

public class ArduinoReader : MonoBehaviour
{

    [HideInInspector]public float potP1 = 0;
    [HideInInspector]public float potP2 = 0;
    [HideInInspector]public float dist = 0;

    //Set parameters according to controller used

    //S
    [NonSerialized] public float potMaxG = 600;
    [NonSerialized] public float potMaxD = 700;
    [NonSerialized] public float potMinG = 300;
    [NonSerialized] public float potMinD = 400;
    [NonSerialized] public float distMax = 170;
    [NonSerialized] public float distMin = 75;

    public bool fullSize = false;

    private SerialPort _stream = new SerialPort("COM3", 9600);
    //private SerialPort _stream = new SerialPort("COM4", 9600);

    private Thread thread;
    private object runArduinoReaderLock = new object();
    private bool _runArduinoReader = true;
    private bool runArduinoReader
    {
        get
        {
            lock (runArduinoReaderLock)
            {
                return _runArduinoReader;
            }
        }
        set
        {
            lock (runArduinoReaderLock)
            {
                _runArduinoReader = value;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        thread = new Thread(ArduinoThread);
        thread.Start();

        if (fullSize)
        {
            //L
            potMaxG = 53;
            potMaxD = 62;
            potMinG = 49;
            potMinD= 57;
            distMax = 36;
            distMin = 3;
        }
        else
        {
            //S
            potMaxG = 53;
            potMaxD = 61;
            potMinG = 48;
            potMinD = 57;
            distMax = 36;
            distMin = 3;
        }
    }
    
    public void ArduinoThread()
    {
        _stream.ReadTimeout = 50;
        _stream.Open();

        double deltaTime = 100;
        double oldTime = 0;
        double currentTime;
        while (runArduinoReader)
        {
            //Do this 10 times per second;
            currentTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
            if(currentTime - oldTime > deltaTime)
            {
                WriteToArduino("INPUT");
                oldTime = currentTime;
            }

            string dataString = null;
            try
            {
                dataString = _stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                DecryptPackage(dataString);
            }
        }
        _stream.Close();
    }

    public void WriteToArduino(string msg)
    {
        _stream.WriteLine(msg);
        _stream.BaseStream.Flush();
    }

    public string ReadFromArduino(int timeout = 0)
    {
        _stream.ReadTimeout = timeout;
        try
        {
            return _stream.ReadLine();
        }
        catch(TimeoutException e)
        {
            return null;

        }
    }

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            try
            {
                dataString = _stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield break; // Terminates the Coroutine
            }
            else
                yield return null; // Wait for next frame

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }

    private void OnDestroy()
    {
        runArduinoReader = false;
        thread.Join();
    }

    private void DecryptPackage(string str)
    {
        string[] words;
        words = str.Split(' ');
        if(words.Length == 6 && words[0] == "d" && words[2] == "x" && words[4] == "y")
        {
            dist = int.Parse(words[1]);
            potP1 = int.Parse(words[3]);
            potP2 = int.Parse(words[5]);

            
            potP2 = Mathf.Min(potP2, potMaxG);
            potP2 = Mathf.Max(potP2, potMinG);
            potP1 = Mathf.Min(potP1, potMaxD);
            potP1 = Mathf.Max(potP1, potMinD);

            dist = Mathf.Min(dist, distMax);
            dist = Mathf.Max(dist, distMin);
            

            //Debug.Log(dist + "__" + potP1 + "__" + potP2);
        }
    }

    
}
