/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_IN_WOOSH = 3654176658U;
        static const AkUniqueID PLAY_INIT = 426840996U;
        static const AkUniqueID PLAY_MELODIE_01 = 3867730695U;
        static const AkUniqueID PLAY_MELODIE_02 = 3867730692U;
        static const AkUniqueID PLAY_MELODIE_03 = 3867730693U;
        static const AkUniqueID PLAY_MELODIE_04 = 3867730690U;
        static const AkUniqueID PLAY_OUT_WOOSH = 4029469807U;
        static const AkUniqueID PLAY_WATERFALL = 467174588U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace FLY_STATE
        {
            static const AkUniqueID GROUP = 4043522470U;

            namespace STATE
            {
                static const AkUniqueID AIR = 1050421051U;
                static const AkUniqueID ARCH = 3652584297U;
                static const AkUniqueID UNDERGROUND = 1543687740U;
            } // namespace STATE
        } // namespace FLY_STATE

        namespace MUSICSTATE
        {
            static const AkUniqueID GROUP = 1021618141U;

            namespace STATE
            {
                static const AkUniqueID AIR = 1050421051U;
                static const AkUniqueID CLOSE = 1451272583U;
                static const AkUniqueID FAR = 1183803292U;
            } // namespace STATE
        } // namespace MUSICSTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace MELODIESELECTER
        {
            static const AkUniqueID GROUP = 3532158187U;

            namespace SWITCH
            {
                static const AkUniqueID MELODIE_01 = 1161931464U;
                static const AkUniqueID MELODIE_02 = 1161931467U;
                static const AkUniqueID MELODIE_03 = 1161931466U;
            } // namespace SWITCH
        } // namespace MELODIESELECTER

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID DOPPLER = 4247512087U;
        static const AkUniqueID LIGHTNUMBERS = 2670876599U;
        static const AkUniqueID PAUSE_GAME = 528278262U;
        static const AkUniqueID RTPC_DISTANCE = 262290038U;
        static const AkUniqueID SIDECHAIN_LIGHT = 3476959006U;
        static const AkUniqueID WIND_MOVE = 1191899237U;
        static const AkUniqueID WIND_PITCH = 2818241024U;
        static const AkUniqueID WIND_SPEED = 3110594711U;
        static const AkUniqueID WIND_YAW = 4252835159U;
        static const AkUniqueID WOOSHPANNING = 2717581126U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID PICKUPLIGHT_1 = 1615847571U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID POTM = 1575124651U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AURO_3D = 2856286951U;
        static const AkUniqueID CHARACHTER = 1750807868U;
        static const AkUniqueID LIGHTS = 3192784746U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MOTION_FACTORY_BUS = 985987111U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SIDECHAIN = 0U;
        static const AkUniqueID WORLD = 2609808943U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID DEFAULT_MOTION_DEVICE = 4230635974U;
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
