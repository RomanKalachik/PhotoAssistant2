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

public class SharpenMicroParams : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SharpenMicroParams(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SharpenMicroParams obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SharpenMicroParams() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_SharpenMicroParams(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public bool enabled {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_enabled_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_enabled_get(swigCPtr);
      return ret;
    } 
  }

  public bool matrix {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_matrix_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_matrix_get(swigCPtr);
      return ret;
    } 
  }

  public double amount {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_amount_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_amount_get(swigCPtr);
      return ret;
    } 
  }

  public double uniformity {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_uniformity_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.SharpenMicroParams_uniformity_get(swigCPtr);
      return ret;
    } 
  }

  public SharpenMicroParams() : this(libPhotoAssistantImageProcessingPINVOKE.new_SharpenMicroParams(), true) {
  }

}

}
