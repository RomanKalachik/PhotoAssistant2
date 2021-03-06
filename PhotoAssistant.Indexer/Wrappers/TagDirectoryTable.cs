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

public class TagDirectoryTable : TagDirectory {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal TagDirectoryTable(global::System.IntPtr cPtr, bool cMemoryOwn) : base(libPhotoAssistantImageProcessingPINVOKE.TagDirectoryTable_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TagDirectoryTable obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~TagDirectoryTable() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_TagDirectoryTable(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public TagDirectoryTable() : this(libPhotoAssistantImageProcessingPINVOKE.new_TagDirectoryTable__SWIG_0(), true) {
  }

  public TagDirectoryTable(TagDirectory p, SWIGTYPE_p_unsigned_char v, int memsize, int offs, TagType type, TagAttrib ta, ByteOrder border) : this(libPhotoAssistantImageProcessingPINVOKE.new_TagDirectoryTable__SWIG_1(TagDirectory.getCPtr(p), SWIGTYPE_p_unsigned_char.getCPtr(v), memsize, offs, (int)type, TagAttrib.getCPtr(ta), (int)border), true) {
  }

  public TagDirectoryTable(TagDirectory p, SWIGTYPE_p_FILE f, int memsize, int offset, TagType type, TagAttrib ta, ByteOrder border) : this(libPhotoAssistantImageProcessingPINVOKE.new_TagDirectoryTable__SWIG_2(TagDirectory.getCPtr(p), SWIGTYPE_p_FILE.getCPtr(f), memsize, offset, (int)type, TagAttrib.getCPtr(ta), (int)border), true) {
  }

  public override int calculateSize() {
    int ret = libPhotoAssistantImageProcessingPINVOKE.TagDirectoryTable_calculateSize(swigCPtr);
    return ret;
  }

  public override int write(int start, SWIGTYPE_p_unsigned_char buffer) {
    int ret = libPhotoAssistantImageProcessingPINVOKE.TagDirectoryTable_write(swigCPtr, start, SWIGTYPE_p_unsigned_char.getCPtr(buffer));
    return ret;
  }

  public override TagDirectory clone(TagDirectory parent) {
    global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.TagDirectoryTable_clone(swigCPtr, TagDirectory.getCPtr(parent));
    TagDirectory ret = (cPtr == global::System.IntPtr.Zero) ? null : new TagDirectory(cPtr, false);
    return ret;
  }

}

}
