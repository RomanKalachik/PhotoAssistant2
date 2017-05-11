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

public class TagAttrib : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal TagAttrib(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TagAttrib obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~TagAttrib() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_TagAttrib(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int ignore {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_ignore_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.TagAttrib_ignore_get(swigCPtr);
      return ret;
    } 
  }

  public ActionCode action {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_action_set(swigCPtr, (int)value);
    } 
    get {
      ActionCode ret = (ActionCode)libPhotoAssistantImageProcessingPINVOKE.TagAttrib_action_get(swigCPtr);
      return ret;
    } 
  }

  public int editable {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_editable_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.TagAttrib_editable_get(swigCPtr);
      return ret;
    } 
  }

  public TagAttrib subdirAttribs {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_subdirAttribs_set(swigCPtr, TagAttrib.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.TagAttrib_subdirAttribs_get(swigCPtr);
      TagAttrib ret = (cPtr == global::System.IntPtr.Zero) ? null : new TagAttrib(cPtr, false);
      return ret;
    } 
  }

  public ushort ID {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_ID_set(swigCPtr, value);
    } 
    get {
      ushort ret = libPhotoAssistantImageProcessingPINVOKE.TagAttrib_ID_get(swigCPtr);
      return ret;
    } 
  }

  public TagType type {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_type_set(swigCPtr, (int)value);
    } 
    get {
      TagType ret = (TagType)libPhotoAssistantImageProcessingPINVOKE.TagAttrib_type_get(swigCPtr);
      return ret;
    } 
  }

  public string name {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_name_set(swigCPtr, value);
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.TagAttrib_name_get(swigCPtr);
      return ret;
    } 
  }

  public Interpreter interpreter {
    set {
      libPhotoAssistantImageProcessingPINVOKE.TagAttrib_interpreter_set(swigCPtr, Interpreter.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.TagAttrib_interpreter_get(swigCPtr);
      Interpreter ret = (cPtr == global::System.IntPtr.Zero) ? null : new Interpreter(cPtr, false);
      return ret;
    } 
  }

  public TagAttrib() : this(libPhotoAssistantImageProcessingPINVOKE.new_TagAttrib(), true) {
  }

}

}
