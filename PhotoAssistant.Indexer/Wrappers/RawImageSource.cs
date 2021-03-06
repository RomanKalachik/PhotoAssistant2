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

public class RawImageSource : ImageSource {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal RawImageSource(global::System.IntPtr cPtr, bool cMemoryOwn) : base(libPhotoAssistantImageProcessingPINVOKE.RawImageSource_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RawImageSource obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RawImageSource() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_RawImageSource(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public RawImageSource() : this(libPhotoAssistantImageProcessingPINVOKE.new_RawImageSource(), true) {
  }

  public override int load(string fname, bool batch) {
    int ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_load__SWIG_0(swigCPtr, fname, batch);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int load(string fname) {
    int ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_load__SWIG_1(swigCPtr, fname);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void preprocess(RAWParams raw, LensProfParams lensProf, CoarseTransformParams coarse) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_preprocess(swigCPtr, RAWParams.getCPtr(raw), LensProfParams.getCPtr(lensProf), CoarseTransformParams.getCPtr(coarse));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void demosaic(RAWParams raw) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_demosaic(swigCPtr, RAWParams.getCPtr(raw));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void flushRawData() {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_flushRawData(swigCPtr);
  }

  public override void flushRGB() {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_flushRGB(swigCPtr);
  }

  public override void HLRecovery_Global(ToneCurveParams hrp) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_HLRecovery_Global(swigCPtr, ToneCurveParams.getCPtr(hrp));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public void refinement_lassus(int PassCount) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_refinement_lassus(swigCPtr, PassCount);
  }

  public void refinement(int PassCount) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_refinement(swigCPtr, PassCount);
  }

  public override bool IsrgbSourceModified() {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_IsrgbSourceModified(swigCPtr);
    return ret;
  }

  public void processFlatField(RAWParams raw, SWIGTYPE_p_RawImage riFlatFile, SWIGTYPE_p_unsigned_short black) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_processFlatField(swigCPtr, RAWParams.getCPtr(raw), SWIGTYPE_p_RawImage.getCPtr(riFlatFile), SWIGTYPE_p_unsigned_short.getCPtr(black));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyOriginalPixels(RAWParams raw, SWIGTYPE_p_RawImage ri, SWIGTYPE_p_RawImage riDark, SWIGTYPE_p_RawImage riFlatFile) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_copyOriginalPixels(swigCPtr, RAWParams.getCPtr(raw), SWIGTYPE_p_RawImage.getCPtr(ri), SWIGTYPE_p_RawImage.getCPtr(riDark), SWIGTYPE_p_RawImage.getCPtr(riFlatFile));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public void cfaboxblur(SWIGTYPE_p_RawImage riFlatFile, SWIGTYPE_p_float cfablur, int boxH, int boxW) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_cfaboxblur(swigCPtr, SWIGTYPE_p_RawImage.getCPtr(riFlatFile), SWIGTYPE_p_float.getCPtr(cfablur), boxH, boxW);
  }

  public void scaleColors(int winx, int winy, int winw, int winh, RAWParams raw) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_scaleColors(swigCPtr, winx, winy, winw, winh, RAWParams.getCPtr(raw));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override SWIGTYPE_p_eSensorType getSensorType() {
    SWIGTYPE_p_eSensorType ret = new SWIGTYPE_p_eSensorType(libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getSensorType(swigCPtr), true);
    return ret;
  }

  public override ColorTemp getWB() {
    ColorTemp ret = new ColorTemp(libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getWB(swigCPtr), true);
    return ret;
  }

  public override void getAutoWBMultipliers(SWIGTYPE_p_double rm, SWIGTYPE_p_double gm, SWIGTYPE_p_double bm) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getAutoWBMultipliers(swigCPtr, SWIGTYPE_p_double.getCPtr(rm), SWIGTYPE_p_double.getCPtr(gm), SWIGTYPE_p_double.getCPtr(bm));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override ColorTemp getSpotWB(SWIGTYPE_p_std__vectorT_rtengine__Coord2D_t red, SWIGTYPE_p_std__vectorT_rtengine__Coord2D_t green, SWIGTYPE_p_std__vectorT_rtengine__Coord2D_t blue, int tran, double equal) {
    ColorTemp ret = new ColorTemp(libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getSpotWB(swigCPtr, SWIGTYPE_p_std__vectorT_rtengine__Coord2D_t.getCPtr(red), SWIGTYPE_p_std__vectorT_rtengine__Coord2D_t.getCPtr(green), SWIGTYPE_p_std__vectorT_rtengine__Coord2D_t.getCPtr(blue), tran, equal), true);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool isWBProviderReady() {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_isWBProviderReady(swigCPtr);
    return ret;
  }

  public override double getDefGain() {
    double ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getDefGain(swigCPtr);
    return ret;
  }

  public override void getFullSize(SWIGTYPE_p_int w, SWIGTYPE_p_int h, int tr) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getFullSize__SWIG_0(swigCPtr, SWIGTYPE_p_int.getCPtr(w), SWIGTYPE_p_int.getCPtr(h), tr);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void getFullSize(SWIGTYPE_p_int w, SWIGTYPE_p_int h) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getFullSize__SWIG_1(swigCPtr, SWIGTYPE_p_int.getCPtr(w), SWIGTYPE_p_int.getCPtr(h));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override int getRotateDegree() {
    int ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getRotateDegree(swigCPtr);
    return ret;
  }

  public override ImageData getImageData() {
    global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getImageData(swigCPtr);
    ImageData ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImageData(cPtr, false);
    return ret;
  }

  public override ImageMatrices getImageMatrices() {
    global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getImageMatrices(swigCPtr);
    ImageMatrices ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImageMatrices(cPtr, false);
    return ret;
  }

  public override bool isRAW() {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_isRAW(swigCPtr);
    return ret;
  }

  public override void setProgressListener(ProgressListener pl) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_setProgressListener(swigCPtr, ProgressListener.getCPtr(pl));
  }

  public override void getAutoExpHistogram(SWIGTYPE_p_LUTT_unsigned_int_t histogram, SWIGTYPE_p_int histcompr) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getAutoExpHistogram(swigCPtr, SWIGTYPE_p_LUTT_unsigned_int_t.getCPtr(histogram), SWIGTYPE_p_int.getCPtr(histcompr));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void getRAWHistogram(SWIGTYPE_p_LUTT_unsigned_int_t histRedRaw, SWIGTYPE_p_LUTT_unsigned_int_t histGreenRaw, SWIGTYPE_p_LUTT_unsigned_int_t histBlueRaw) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getRAWHistogram(swigCPtr, SWIGTYPE_p_LUTT_unsigned_int_t.getCPtr(histRedRaw), SWIGTYPE_p_LUTT_unsigned_int_t.getCPtr(histGreenRaw), SWIGTYPE_p_LUTT_unsigned_int_t.getCPtr(histBlueRaw));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public override DCPProfile getDCP(ColorManagementParams cmp, ColorTemp wb) {
    global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_getDCP(swigCPtr, ColorManagementParams.getCPtr(cmp), ColorTemp.getCPtr(wb));
    DCPProfile ret = (cPtr == global::System.IntPtr.Zero) ? null : new DCPProfile(cPtr, false);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void convertColorSpace(Imagefloat image, ColorManagementParams cmp, ColorTemp wb) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_convertColorSpace(swigCPtr, Imagefloat.getCPtr(image), ColorManagementParams.getCPtr(cmp), ColorTemp.getCPtr(wb));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static bool findInputProfile(string inProfile, SWIGTYPE_p_cmsHPROFILE embedded, string camName, SWIGTYPE_p_p_rtengine__DCPProfile dcpProf, SWIGTYPE_p_cmsHPROFILE arg4) {
    bool ret = libPhotoAssistantImageProcessingPINVOKE.RawImageSource_findInputProfile(inProfile, SWIGTYPE_p_cmsHPROFILE.getCPtr(embedded), camName, SWIGTYPE_p_p_rtengine__DCPProfile.getCPtr(dcpProf), SWIGTYPE_p_cmsHPROFILE.getCPtr(arg4));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void colorSpaceConversion(Imagefloat im, ColorManagementParams cmp, ColorTemp wb, SWIGTYPE_p_double pre_mul, SWIGTYPE_p_cmsHPROFILE embedded, SWIGTYPE_p_cmsHPROFILE camprofile, SWIGTYPE_p_a_3__double cam, string camName) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_colorSpaceConversion(Imagefloat.getCPtr(im), ColorManagementParams.getCPtr(cmp), ColorTemp.getCPtr(wb), SWIGTYPE_p_double.getCPtr(pre_mul), SWIGTYPE_p_cmsHPROFILE.getCPtr(embedded), SWIGTYPE_p_cmsHPROFILE.getCPtr(camprofile), SWIGTYPE_p_a_3__double.getCPtr(cam), camName);
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void inverse33(SWIGTYPE_p_a_3__double coeff, SWIGTYPE_p_a_3__double icoeff) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_inverse33(SWIGTYPE_p_a_3__double.getCPtr(coeff), SWIGTYPE_p_a_3__double.getCPtr(icoeff));
  }

  public void boxblur2(SWIGTYPE_p_p_float src, SWIGTYPE_p_p_float dst, int H, int W, int box) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_boxblur2(swigCPtr, SWIGTYPE_p_p_float.getCPtr(src), SWIGTYPE_p_p_float.getCPtr(dst), H, W, box);
  }

  public void boxblur_resamp(SWIGTYPE_p_p_float src, SWIGTYPE_p_p_float dst, int H, int W, int box, int samp) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_boxblur_resamp(swigCPtr, SWIGTYPE_p_p_float.getCPtr(src), SWIGTYPE_p_p_float.getCPtr(dst), H, W, box, samp);
  }

  public override void HLRecovery_inpaint(SWIGTYPE_p_p_float red, SWIGTYPE_p_p_float green, SWIGTYPE_p_p_float blue) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_HLRecovery_inpaint(swigCPtr, SWIGTYPE_p_p_float.getCPtr(red), SWIGTYPE_p_p_float.getCPtr(green), SWIGTYPE_p_p_float.getCPtr(blue));
  }

  public static void HLRecovery_Luminance(SWIGTYPE_p_float rin, SWIGTYPE_p_float gin, SWIGTYPE_p_float bin, SWIGTYPE_p_float rout, SWIGTYPE_p_float gout, SWIGTYPE_p_float bout, int width, float maxval) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_HLRecovery_Luminance(SWIGTYPE_p_float.getCPtr(rin), SWIGTYPE_p_float.getCPtr(gin), SWIGTYPE_p_float.getCPtr(bin), SWIGTYPE_p_float.getCPtr(rout), SWIGTYPE_p_float.getCPtr(gout), SWIGTYPE_p_float.getCPtr(bout), width, maxval);
  }

  public static void HLRecovery_CIELab(SWIGTYPE_p_float rin, SWIGTYPE_p_float gin, SWIGTYPE_p_float bin, SWIGTYPE_p_float rout, SWIGTYPE_p_float gout, SWIGTYPE_p_float bout, int width, float maxval, SWIGTYPE_p_a_3__double cam, SWIGTYPE_p_a_3__double icam) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_HLRecovery_CIELab(SWIGTYPE_p_float.getCPtr(rin), SWIGTYPE_p_float.getCPtr(gin), SWIGTYPE_p_float.getCPtr(bin), SWIGTYPE_p_float.getCPtr(rout), SWIGTYPE_p_float.getCPtr(gout), SWIGTYPE_p_float.getCPtr(bout), width, maxval, SWIGTYPE_p_a_3__double.getCPtr(cam), SWIGTYPE_p_a_3__double.getCPtr(icam));
  }

  public static void HLRecovery_blend(SWIGTYPE_p_float rin, SWIGTYPE_p_float gin, SWIGTYPE_p_float bin, int width, float maxval, SWIGTYPE_p_float hlmax) {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_HLRecovery_blend(SWIGTYPE_p_float.getCPtr(rin), SWIGTYPE_p_float.getCPtr(gin), SWIGTYPE_p_float.getCPtr(bin), width, maxval, SWIGTYPE_p_float.getCPtr(hlmax));
  }

  public static void init() {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_init();
  }

  public static void cleanup() {
    libPhotoAssistantImageProcessingPINVOKE.RawImageSource_cleanup();
  }

}

}
