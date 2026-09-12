using System;
using System.Drawing;

namespace Alpha.Classes
{
    [Serializable]
    public class WebCamClass
    {
        private string attDeviceName, attSerialNumber;
        private Bitmap attimgBuffer;
        private int attNextStagePosition, attStageNumber, attFocus, attBrightness, attExposure, attZoom, attGain, attWhiteBalance;
        private AForge.Video.DirectShow.CameraControlFlags attFocusFlag, attBrightnessFlag, attExposureFlag, attZoomFlag, attGainFlag, attWhiteBalanceFlag;

        public string deviceName
        {
            get { return attDeviceName; }
            set { attDeviceName = value; }
        }
        public string serialNumber
        {
            get { return attSerialNumber; }
            set { attSerialNumber = value; }
        }
        public Bitmap imgBuffer
        {
            get { return attimgBuffer; }
            set { attimgBuffer = value; }
        }
        public int nextStagePosition
        {
            get { return attNextStagePosition; }
            set { attNextStagePosition = value; }
        }
        public int stageNumber
        {
            get { return attStageNumber; }
            set { attStageNumber = value; }
        }
        public int focus
        {
            get { return attFocus; }
            set { attFocus = value; }
        }
        public AForge.Video.DirectShow.CameraControlFlags focusFlag
        {
            get { return attFocusFlag; }
            set { attFocusFlag = value; }
        }
        public int brightness
        {
            get { return attBrightness; }
            set { attBrightness = value; }
        }
        public AForge.Video.DirectShow.CameraControlFlags brightnessFlag
        {
            get { return attBrightnessFlag; }
            set { attBrightnessFlag = value; }
        }
        public int exposure
        {
            get { return attExposure; }
            set { attExposure = value; }
        }
        public AForge.Video.DirectShow.CameraControlFlags exposureFlag
        {
            get { return attExposureFlag; }
            set { attExposureFlag = value; }
        }
        public int zoom
        {
            get { return attZoom; }
            set { attZoom = value; }
        }
        public AForge.Video.DirectShow.CameraControlFlags zoomFlag
        {
            get { return attZoomFlag; }
            set { attZoomFlag = value; }
        }
        public int gain
        {
            get { return attGain; }
            set { attGain = value; }
        }
        public AForge.Video.DirectShow.CameraControlFlags gainFlag
        {
            get { return attGainFlag; }
            set { attGainFlag = value; }
        }
        public int whiteBalance
        {
            get { return attWhiteBalance; }
            set { attWhiteBalance = value; }
        }
        public AForge.Video.DirectShow.CameraControlFlags whiteBalanceFlag
        {
            get { return attWhiteBalanceFlag; }
            set { attWhiteBalanceFlag = value; }
        }
        public WebCamClass()
        {
            attDeviceName = "";
            attSerialNumber = "";
            attNextStagePosition = -1;
            attStageNumber = 0;
            attFocus = 0;
            attFocusFlag = AForge.Video.DirectShow.CameraControlFlags.None;
            attZoom = 100;
            attZoomFlag = AForge.Video.DirectShow.CameraControlFlags.None;
            attBrightness = 128;
            attBrightnessFlag = AForge.Video.DirectShow.CameraControlFlags.None;
            attWhiteBalance = 4000;
            attWhiteBalanceFlag = AForge.Video.DirectShow.CameraControlFlags.None;
            attExposure = -4;
            attExposureFlag = AForge.Video.DirectShow.CameraControlFlags.None;
            attGain = 0;
            attGainFlag = AForge.Video.DirectShow.CameraControlFlags.None;
        }
    }
}

