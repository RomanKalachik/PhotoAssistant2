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

public class WaveletParams : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal WaveletParams(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(WaveletParams obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~WaveletParams() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libPhotoAssistantImageProcessingPINVOKE.delete_WaveletParams(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public SWIGTYPE_p_std__vectorT_double_t ccwcurve {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_ccwcurve_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_ccwcurve_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t opacityCurveRG {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveRG_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveRG_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t opacityCurveBY {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveBY_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveBY_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t opacityCurveW {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveW_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveW_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t opacityCurveWL {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveWL_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_opacityCurveWL_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t hhcurve {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hhcurve_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hhcurve_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t Chcurve {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Chcurve_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Chcurve_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_double_t wavclCurve {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_wavclCurve_set(swigCPtr, SWIGTYPE_p_std__vectorT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_wavclCurve_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_double_t(cPtr, false);
      return ret;
    } 
  }

  public bool enabled {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_enabled_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_enabled_get(swigCPtr);
      return ret;
    } 
  }

  public bool median {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_median_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_median_get(swigCPtr);
      return ret;
    } 
  }

  public bool medianlev {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_medianlev_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_medianlev_get(swigCPtr);
      return ret;
    } 
  }

  public bool linkedg {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_linkedg_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_linkedg_get(swigCPtr);
      return ret;
    } 
  }

  public bool cbenab {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_cbenab_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_cbenab_get(swigCPtr);
      return ret;
    } 
  }

  public double greenlow {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_greenlow_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_greenlow_get(swigCPtr);
      return ret;
    } 
  }

  public double bluelow {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bluelow_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bluelow_get(swigCPtr);
      return ret;
    } 
  }

  public double greenmed {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_greenmed_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_greenmed_get(swigCPtr);
      return ret;
    } 
  }

  public double bluemed {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bluemed_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bluemed_get(swigCPtr);
      return ret;
    } 
  }

  public double greenhigh {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_greenhigh_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_greenhigh_get(swigCPtr);
      return ret;
    } 
  }

  public double bluehigh {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bluehigh_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bluehigh_get(swigCPtr);
      return ret;
    } 
  }

  public bool lipst {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_lipst_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_lipst_get(swigCPtr);
      return ret;
    } 
  }

  public bool avoid {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_avoid_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_avoid_get(swigCPtr);
      return ret;
    } 
  }

  public bool tmr {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_tmr_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_tmr_get(swigCPtr);
      return ret;
    } 
  }

  public int strength {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_strength_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_strength_get(swigCPtr);
      return ret;
    } 
  }

  public int balance {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_balance_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_balance_get(swigCPtr);
      return ret;
    } 
  }

  public int iter {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_iter_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_iter_get(swigCPtr);
      return ret;
    } 
  }

  public bool expcontrast {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expcontrast_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expcontrast_get(swigCPtr);
      return ret;
    } 
  }

  public bool expchroma {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expchroma_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expchroma_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_int c {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_c_set(swigCPtr, SWIGTYPE_p_int.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_c_get(swigCPtr);
      SWIGTYPE_p_int ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_int(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_int ch {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_ch_set(swigCPtr, SWIGTYPE_p_int.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_ch_get(swigCPtr);
      SWIGTYPE_p_int ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_int(cPtr, false);
      return ret;
    } 
  }

  public bool expedge {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expedge_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expedge_get(swigCPtr);
      return ret;
    } 
  }

  public bool expresid {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expresid_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expresid_get(swigCPtr);
      return ret;
    } 
  }

  public bool expfinal {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expfinal_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expfinal_get(swigCPtr);
      return ret;
    } 
  }

  public bool exptoning {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_exptoning_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_exptoning_get(swigCPtr);
      return ret;
    } 
  }

  public bool expnoise {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expnoise_set(swigCPtr, value);
    } 
    get {
      bool ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_expnoise_get(swigCPtr);
      return ret;
    } 
  }

  public string Lmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Lmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Lmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string CLmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_CLmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_CLmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string Backmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Backmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Backmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string Tilesmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Tilesmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Tilesmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string daubcoeffmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_daubcoeffmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_daubcoeffmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string CHmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_CHmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_CHmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string Medgreinf {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Medgreinf_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Medgreinf_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string CHSLmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_CHSLmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_CHSLmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string EDmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_EDmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_EDmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string NPmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_NPmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_NPmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string BAmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_BAmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_BAmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string TMmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_TMmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_TMmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string Dirmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Dirmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_Dirmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string HSmethod {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_HSmethod_set(swigCPtr, value);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_HSmethod_get(swigCPtr);
      if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int rescon {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_rescon_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_rescon_get(swigCPtr);
      return ret;
    } 
  }

  public int resconH {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_resconH_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_resconH_get(swigCPtr);
      return ret;
    } 
  }

  public int reschro {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_reschro_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_reschro_get(swigCPtr);
      return ret;
    } 
  }

  public double tmrs {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_tmrs_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_tmrs_get(swigCPtr);
      return ret;
    } 
  }

  public double gamma {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_gamma_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_gamma_get(swigCPtr);
      return ret;
    } 
  }

  public int sup {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_sup_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_sup_get(swigCPtr);
      return ret;
    } 
  }

  public double sky {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_sky_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_sky_get(swigCPtr);
      return ret;
    } 
  }

  public int thres {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_thres_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_thres_get(swigCPtr);
      return ret;
    } 
  }

  public int chroma {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_chroma_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_chroma_get(swigCPtr);
      return ret;
    } 
  }

  public int chro {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_chro_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_chro_get(swigCPtr);
      return ret;
    } 
  }

  public int threshold {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_threshold_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_threshold_get(swigCPtr);
      return ret;
    } 
  }

  public int threshold2 {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_threshold2_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_threshold2_get(swigCPtr);
      return ret;
    } 
  }

  public int edgedetect {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgedetect_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgedetect_get(swigCPtr);
      return ret;
    } 
  }

  public int edgedetectthr {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgedetectthr_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgedetectthr_get(swigCPtr);
      return ret;
    } 
  }

  public int edgedetectthr2 {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgedetectthr2_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgedetectthr2_get(swigCPtr);
      return ret;
    } 
  }

  public int edgesensi {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgesensi_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgesensi_get(swigCPtr);
      return ret;
    } 
  }

  public int edgeampli {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgeampli_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgeampli_get(swigCPtr);
      return ret;
    } 
  }

  public int contrast {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_contrast_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_contrast_get(swigCPtr);
      return ret;
    } 
  }

  public int edgrad {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgrad_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgrad_get(swigCPtr);
      return ret;
    } 
  }

  public int edgval {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgval_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgval_get(swigCPtr);
      return ret;
    } 
  }

  public int edgthresh {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgthresh_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgthresh_get(swigCPtr);
      return ret;
    } 
  }

  public int thr {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_thr_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_thr_get(swigCPtr);
      return ret;
    } 
  }

  public int thrH {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_thrH_set(swigCPtr, value);
    } 
    get {
      int ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_thrH_get(swigCPtr);
      return ret;
    } 
  }

  public double skinprotect {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_skinprotect_set(swigCPtr, value);
    } 
    get {
      double ret = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_skinprotect_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t hueskin {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hueskin_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hueskin_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t hueskin2 {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hueskin2_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hueskin2_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t hllev {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hllev_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_hllev_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t bllev {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bllev_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_bllev_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t pastlev {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_pastlev_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_pastlev_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t satlev {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_satlev_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_satlev_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t edgcont {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgcont_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_edgcont_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t level0noise {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level0noise_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level0noise_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t level1noise {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level1noise_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level1noise_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t level2noise {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level2noise_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level2noise_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t level3noise {
    set {
      libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level3noise_set(swigCPtr, SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = libPhotoAssistantImageProcessingPINVOKE.WaveletParams_level3noise_get(swigCPtr);
      SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_rtengine__procparams__ThresholdT_double_t(cPtr, false);
      return ret;
    } 
  }

  public WaveletParams() : this(libPhotoAssistantImageProcessingPINVOKE.new_WaveletParams(), true) {
  }

  public void setDefaults() {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_setDefaults(swigCPtr);
  }

  public void getCurves(SWIGTYPE_p_rtengine__WavCurve cCurve, SWIGTYPE_p_rtengine__WavOpacityCurveRG opacityCurveLUTRG, SWIGTYPE_p_rtengine__WavOpacityCurveBY opacityCurveLUTBY, SWIGTYPE_p_rtengine__WavOpacityCurveW opacityCurveLUTW, SWIGTYPE_p_rtengine__WavOpacityCurveWL opacityCurveLUTWL) {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_getCurves(swigCPtr, SWIGTYPE_p_rtengine__WavCurve.getCPtr(cCurve), SWIGTYPE_p_rtengine__WavOpacityCurveRG.getCPtr(opacityCurveLUTRG), SWIGTYPE_p_rtengine__WavOpacityCurveBY.getCPtr(opacityCurveLUTBY), SWIGTYPE_p_rtengine__WavOpacityCurveW.getCPtr(opacityCurveLUTW), SWIGTYPE_p_rtengine__WavOpacityCurveWL.getCPtr(opacityCurveLUTWL));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void getDefaultCCWCurve(SWIGTYPE_p_std__vectorT_double_t curve) {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_getDefaultCCWCurve(SWIGTYPE_p_std__vectorT_double_t.getCPtr(curve));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void getDefaultOpacityCurveRG(SWIGTYPE_p_std__vectorT_double_t curve) {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_getDefaultOpacityCurveRG(SWIGTYPE_p_std__vectorT_double_t.getCPtr(curve));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void getDefaultOpacityCurveBY(SWIGTYPE_p_std__vectorT_double_t curve) {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_getDefaultOpacityCurveBY(SWIGTYPE_p_std__vectorT_double_t.getCPtr(curve));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void getDefaultOpacityCurveW(SWIGTYPE_p_std__vectorT_double_t curve) {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_getDefaultOpacityCurveW(SWIGTYPE_p_std__vectorT_double_t.getCPtr(curve));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void getDefaultOpacityCurveWL(SWIGTYPE_p_std__vectorT_double_t curve) {
    libPhotoAssistantImageProcessingPINVOKE.WaveletParams_getDefaultOpacityCurveWL(SWIGTYPE_p_std__vectorT_double_t.getCPtr(curve));
    if (libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Pending) throw libPhotoAssistantImageProcessingPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
