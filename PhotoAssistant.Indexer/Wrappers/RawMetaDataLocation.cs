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

public class RawMetaDataLocation : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal RawMetaDataLocation(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RawMetaDataLocation obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RawMetaDataLocation() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_RawMetaDataLocation(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int exifBase {
    set {
      libPhotoAssistantImageProcessingPINVOKE.RawMetaDataLocation_exifBase_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.RawMetaDataLocation_exifBase_get(swigCPtr);
      return ret;
    } 
  }

  public int ciffBase {
    set {
      libPhotoAssistantImageProcessingPINVOKE.RawMetaDataLocation_ciffBase_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.RawMetaDataLocation_ciffBase_get(swigCPtr);
      return ret;
    } 
  }

  public int ciffLength {
    set {
      libPhotoAssistantImageProcessingPINVOKE.RawMetaDataLocation_ciffLength_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.RawMetaDataLocation_ciffLength_get(swigCPtr);
      return ret;
    } 
  }

  public RawMetaDataLocation() : this(libPhotoAssistantImageProcessingPINVOKE.new_RawMetaDataLocation(), true) {
  }

}

}
