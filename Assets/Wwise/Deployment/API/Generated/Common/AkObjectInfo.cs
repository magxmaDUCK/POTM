#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkObjectInfo : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkObjectInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkObjectInfo obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkObjectInfo() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkObjectInfo(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public uint objID { set { AkSoundEnginePINVOKE.CSharp_AkObjectInfo_objID_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkObjectInfo_objID_get(swigCPtr); } 
  }

  public uint parentID { set { AkSoundEnginePINVOKE.CSharp_AkObjectInfo_parentID_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkObjectInfo_parentID_get(swigCPtr); } 
  }

  public int iDepth { set { AkSoundEnginePINVOKE.CSharp_AkObjectInfo_iDepth_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkObjectInfo_iDepth_get(swigCPtr); } 
  }

  public AkObjectInfo() : this(AkSoundEnginePINVOKE.CSharp_new_AkObjectInfo(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.