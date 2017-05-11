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

public class PerspectiveParams : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PerspectiveParams(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(PerspectiveParams obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~PerspectiveParams() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_PerspectiveParams(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public double horizontal {
    set {
      libPhotoAssistantImageProcessingPINVOKE.PerspectiveParams_horizontal_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.PerspectiveParams_horizontal_get(swigCPtr);
      return ret;
    } 
  }

  public double vertical {
    set {
      libPhotoAssistantImageProcessingPINVOKE.PerspectiveParams_vertical_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.PerspectiveParams_vertical_get(swigCPtr);
      return ret;
    } 
  }

  public PerspectiveParams() : this(libPhotoAssistantImageProcessingPINVOKE.new_PerspectiveParams(), true) {
  }

}

}
