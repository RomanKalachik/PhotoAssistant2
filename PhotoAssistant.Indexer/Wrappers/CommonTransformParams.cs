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

public class CommonTransformParams : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CommonTransformParams(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CommonTransformParams obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CommonTransformParams() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_CommonTransformParams(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public bool autofill {
    set {
      libPhotoAssistantImageProcessingPINVOKE.CommonTransformParams_autofill_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.CommonTransformParams_autofill_get(swigCPtr);
      return ret;
    } 
  }

  public CommonTransformParams() : this(libPhotoAssistantImageProcessingPINVOKE.new_CommonTransformParams(), true) {
  }

}

}
