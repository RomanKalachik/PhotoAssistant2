//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.7
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace PhotoAssistant.Indexer.Wrappers {

public class ImageMetaData : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImageMetaData(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImageMetaData obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ImageMetaData() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_ImageMetaData(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public virtual bool hasExif() {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_hasExif(swigCPtr);
    return ret;
  }

  public virtual TagDirectory getExifData() {
    global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getExifData(swigCPtr);
    TagDirectory ret = (cPtr == global::System.IntPtr.Zero) ? null : new TagDirectory(cPtr, false);
    return ret;
  }

  public virtual bool hasIPTC() {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_hasIPTC(swigCPtr);
    return ret;
  }

  public virtual SWIGTYPE_p_std__mapT_Glib__ustring_std__vectorT_Glib__ustring_t_std__lessT_Glib__ustring_t_t getIPTCData() {
    SWIGTYPE_p_std__mapT_Glib__ustring_std__vectorT_Glib__ustring_t_std__lessT_Glib__ustring_t_t ret = new SWIGTYPE_p_std__mapT_Glib__ustring_std__vectorT_Glib__ustring_t_std__lessT_Glib__ustring_t_t(libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getIPTCData(swigCPtr), true);
    return ret;
  }

  public virtual SWIGTYPE_p_tm getDateTime() {
    SWIGTYPE_p_tm ret = new SWIGTYPE_p_tm(libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getDateTime(swigCPtr), true);
    return ret;
  }

  public virtual SWIGTYPE_p_time_t getDateTimeAsTS() {
    SWIGTYPE_p_time_t ret = new SWIGTYPE_p_time_t(libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getDateTimeAsTS(swigCPtr), true);
    return ret;
  }

  public virtual int getISOSpeed() {
    int ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getISOSpeed(swigCPtr);
    return ret;
  }

  public virtual double getFNumber() {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getFNumber(swigCPtr);
    return ret;
  }

  public virtual double getFocalLen() {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getFocalLen(swigCPtr);
    return ret;
  }

  public virtual double getFocalLen35mm() {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getFocalLen35mm(swigCPtr);
    return ret;
  }

  public virtual float getFocusDist() {
    float ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getFocusDist(swigCPtr);
    return ret;
  }

  public virtual double getShutterSpeed() {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getShutterSpeed(swigCPtr);
    return ret;
  }

  public virtual double getExpComp() {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getExpComp(swigCPtr);
    return ret;
  }

  public virtual string getMake() {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getMake(swigCPtr);
    return ret;
  }

  public virtual string getModel() {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getModel(swigCPtr);
    return ret;
  }

  public string getCamera() {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getCamera(swigCPtr);
    return ret;
  }

  public virtual string getLens() {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getLens(swigCPtr);
    return ret;
  }

  public virtual string getOrientation() {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_getOrientation(swigCPtr);
    return ret;
  }

  public static string apertureToString(double aperture) {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_apertureToString(aperture);
    return ret;
  }

  public static string shutterToString(double shutter) {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_shutterToString(shutter);
    return ret;
  }

  public static double apertureFromString(string shutter) {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_apertureFromString(shutter);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double shutterFromString(string shutter) {
    double ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_shutterFromString(shutter);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string expcompToString(double expcomp, bool maskZeroexpcomp) {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_expcompToString(expcomp, maskZeroexpcomp);
    return ret;
  }

  public static ImageMetaData fromFile(string fname, RawMetaDataLocation rml) {
    global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.ImageMetaData_fromFile(fname, RawMetaDataLocation.getCPtr(rml));
    ImageMetaData ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImageMetaData(cPtr, false);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
