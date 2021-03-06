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

public class ImageDatas : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImageDatas(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImageDatas obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ImageDatas() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_ImageDatas(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public virtual void allocate(int W, int H) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_allocate(swigCPtr, W, H);
  }

  public virtual void rotate(int deg) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_rotate(swigCPtr, deg);
  }

  public virtual void flushData() {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_flushData(swigCPtr);
  }

  public virtual void hflip() {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_hflip(swigCPtr);
  }

  public virtual void vflip() {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_vflip(swigCPtr);
  }

  public void readData(SWIGTYPE_p_FILE fh) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_readData(swigCPtr, SWIGTYPE_p_FILE.getCPtr(fh));
  }

  public void writeData(SWIGTYPE_p_FILE fh) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_writeData(swigCPtr, SWIGTYPE_p_FILE.getCPtr(fh));
  }

  public virtual void normalizeInt(int srcMinVal, int srcMaxVal) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_normalizeInt(swigCPtr, srcMinVal, srcMaxVal);
  }

  public virtual void normalizeFloat(float srcMinVal, float srcMaxVal) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_normalizeFloat(swigCPtr, srcMinVal, srcMaxVal);
  }

  public virtual void computeHistogramAutoWB(SWIGTYPE_p_double avg_r, SWIGTYPE_p_double avg_g, SWIGTYPE_p_double avg_b, SWIGTYPE_p_int n, SWIGTYPE_p_LUTu histogram, int compression) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_computeHistogramAutoWB(swigCPtr, SWIGTYPE_p_double.getCPtr(avg_r), SWIGTYPE_p_double.getCPtr(avg_g), SWIGTYPE_p_double.getCPtr(avg_b), SWIGTYPE_p_int.getCPtr(n), SWIGTYPE_p_LUTu.getCPtr(histogram), compression);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void getSpotWBData(SWIGTYPE_p_double reds, SWIGTYPE_p_double greens, SWIGTYPE_p_double blues, SWIGTYPE_p_int rn, SWIGTYPE_p_int gn, SWIGTYPE_p_int bn, SWIGTYPE_p_std__vectorT_Coord2D_t red, SWIGTYPE_p_std__vectorT_Coord2D_t green, SWIGTYPE_p_std__vectorT_Coord2D_t blue, int tran) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_getSpotWBData(swigCPtr, SWIGTYPE_p_double.getCPtr(reds), SWIGTYPE_p_double.getCPtr(greens), SWIGTYPE_p_double.getCPtr(blues), SWIGTYPE_p_int.getCPtr(rn), SWIGTYPE_p_int.getCPtr(gn), SWIGTYPE_p_int.getCPtr(bn), SWIGTYPE_p_std__vectorT_Coord2D_t.getCPtr(red), SWIGTYPE_p_std__vectorT_Coord2D_t.getCPtr(green), SWIGTYPE_p_std__vectorT_Coord2D_t.getCPtr(blue), tran);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void getAutoWBMultipliers(SWIGTYPE_p_double rm, SWIGTYPE_p_double gm, SWIGTYPE_p_double bm) {
    libPhotoAssistantImageProcessingPINVOKE.ImageDatas_getAutoWBMultipliers(swigCPtr, SWIGTYPE_p_double.getCPtr(rm), SWIGTYPE_p_double.getCPtr(gm), SWIGTYPE_p_double.getCPtr(bm));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual string getType() {
    string ret = libPhotoAssistantImageProcessingPINVOKE.ImageDatas_getType(swigCPtr);
    return ret;
  }

  public ImageDatas() : this(libPhotoAssistantImageProcessingPINVOKE.new_ImageDatas(), true) {
  }

}

}
