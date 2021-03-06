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

public class DetailedCrop : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DetailedCrop(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DetailedCrop obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~DetailedCrop() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_DetailedCrop(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public virtual void setWindow(int cx, int cy, int cw, int ch, int skip) {
    libPhotoAssistantImageProcessingPINVOKE.DetailedCrop_setWindow(swigCPtr, cx, cy, cw, ch, skip);
  }

  public virtual bool tryUpdate() {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.DetailedCrop_tryUpdate(swigCPtr);
    return ret;
  }

  public virtual void fullUpdate() {
    libPhotoAssistantImageProcessingPINVOKE.DetailedCrop_fullUpdate(swigCPtr);
  }

  public virtual void setListener(DetailedCropListener il) {
    libPhotoAssistantImageProcessingPINVOKE.DetailedCrop_setListener(swigCPtr, DetailedCropListener.getCPtr(il));
  }

  public virtual void destroy() {
    libPhotoAssistantImageProcessingPINVOKE.DetailedCrop_destroy(swigCPtr);
  }

  public DetailedCrop() : this(libPhotoAssistantImageProcessingPINVOKE.new_DetailedCrop(), true) {
  }

}

}
