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

public class SaveFormat : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SaveFormat(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SaveFormat obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SaveFormat() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_SaveFormat(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public string format {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_format_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_format_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int pngBits {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_pngBits_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_pngBits_get(swigCPtr);
      return ret;
    } 
  }

  public int pngCompression {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_pngCompression_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_pngCompression_get(swigCPtr);
      return ret;
    } 
  }

  public int jpegQuality {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_jpegQuality_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_jpegQuality_get(swigCPtr);
      return ret;
    } 
  }

  public int jpegSubSamp {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_jpegSubSamp_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_jpegSubSamp_get(swigCPtr);
      return ret;
    } 
  }

  public int tiffBits {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_tiffBits_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_tiffBits_get(swigCPtr);
      return ret;
    } 
  }

  public bool tiffUncompressed {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_tiffUncompressed_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_tiffUncompressed_get(swigCPtr);
      return ret;
    } 
  }

  public bool saveParams {
    set {
      libPhotoAssistantImageProcessingPINVOKE.SaveFormat_saveParams_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.SaveFormat_saveParams_get(swigCPtr);
      return ret;
    } 
  }

  public SaveFormat() : this(libPhotoAssistantImageProcessingPINVOKE.new_SaveFormat(), true) {
  }

}

}
