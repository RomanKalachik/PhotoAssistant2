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

public class GradientParams : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GradientParams(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GradientParams obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~GradientParams() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_GradientParams(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public bool enabled {
    set {
      libPhotoAssistantImageProcessingPINVOKE.GradientParams_enabled_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.GradientParams_enabled_get(swigCPtr);
      return ret;
    } 
  }

  public double degree {
    set {
      libPhotoAssistantImageProcessingPINVOKE.GradientParams_degree_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.GradientParams_degree_get(swigCPtr);
      return ret;
    } 
  }

  public int feather {
    set {
      libPhotoAssistantImageProcessingPINVOKE.GradientParams_feather_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.GradientParams_feather_get(swigCPtr);
      return ret;
    } 
  }

  public double strength {
    set {
      libPhotoAssistantImageProcessingPINVOKE.GradientParams_strength_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.GradientParams_strength_get(swigCPtr);
      return ret;
    } 
  }

  public int centerX {
    set {
      libPhotoAssistantImageProcessingPINVOKE.GradientParams_centerX_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.GradientParams_centerX_get(swigCPtr);
      return ret;
    } 
  }

  public int centerY {
    set {
      libPhotoAssistantImageProcessingPINVOKE.GradientParams_centerY_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.GradientParams_centerY_get(swigCPtr);
      return ret;
    } 
  }

  public GradientParams() : this(libPhotoAssistantImageProcessingPINVOKE.new_GradientParams(), true) {
  }

}

}
