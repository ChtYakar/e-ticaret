using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MvcUI.ConDmnsServis;
using WCFServisler;

namespace MvcUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Login(string eposta, string sifre)
        {
            if (!string.IsNullOrEmpty(eposta) && !string.IsNullOrEmpty(sifre))
            {
                using (ConDmnsServis.DmnsServisClient sdb = new DmnsServisClient())
                {
                    string kisi = sdb.Login(eposta, sifre);
                    if (kisi != null)
                    {
                        Kisiler _kisi = new JavaScriptSerializer().Deserialize<Kisiler>(kisi);
                        Session["CurrentUser"] = _kisi;
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Urunler()
        {
            using (ConDmnsServis.DmnsServisClient sdb = new DmnsServisClient())
            {
                string urunList = sdb.Urunler();
                if (urunList != null)
                {
                    List<Urunler> urunler = new JavaScriptSerializer().Deserialize<List<Urunler>>(urunList);
                    return View(urunler);
                }
            }
            return View();
        }
        List<Urunler> _sepetUrunListesi = new List<Urunler>();

        [HttpGet]
        public ActionResult Sepet(int urunId = 0, bool delete = false)
        {
            if (Session["SepetUrunleri"] != null)
            {
                _sepetUrunListesi = (List<Urunler>)Session["SepetUrunleri"];
            }
            if (urunId > 0)
            {
               

                if (!delete)
                {
                    using (ConDmnsServis.DmnsServisClient sdb = new DmnsServisClient())
                    {
                        var urun = sdb.UrunGetir(urunId);
                        if (_sepetUrunListesi.All(x => x.UrunId != urunId))
                        {
                            _sepetUrunListesi.Add(new JavaScriptSerializer().Deserialize<Urunler>(urun));
                        }
                        Session["SepetUrunleri"] = _sepetUrunListesi;
                        return View();
                    }
                }
                else
                {
                    _sepetUrunListesi.Remove(_sepetUrunListesi.Find(x => x.UrunId == urunId));
                    Session["SepetUrunleri"] = (!_sepetUrunListesi.Any() ? null : _sepetUrunListesi) ;
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public class UrunAdet
        {
            public int Adet { get; set; }
            public int UrunId { get; set; }
        }
        public JsonResult SiparisOlustur(string data)
        {
            String mesaj = "";
            if (Session["CurrentUser"] == null)
            {
                return Json(new {sonuc = false});
            }
            if (Session["SepetUrunleri"] == null)
            {
                return Json("Sepetinizde ürün bulunmamaktadır.");
            }
            List<Urunler> sepet = (List<Urunler>)Session["SepetUrunleri"];
            using (ConDmnsServis.DmnsServisClient sdb = new DmnsServisClient())
            {
                if (!String.IsNullOrEmpty(data))
                {
                    List<Siparisler> siparislistesi = new List<Siparisler>();
                   List<UrunAdet>_urunAdet = new JavaScriptSerializer().Deserialize<List<UrunAdet>>(data);
                 
                   foreach (var sipUrunAdet in _urunAdet)
                   {
                       var firstOrDefault = sepet.FirstOrDefault(x => x.UrunId == sipUrunAdet.UrunId);
                       if (firstOrDefault != null)
                       {
                           Siparisler siparis = new Siparisler()
                           {
                               KisiId = ((Kisiler) Session["CurrentUser"]).KisiId,
                               UrunId = sipUrunAdet.UrunId,
                               Quantity = sipUrunAdet.Adet,
                               ToplamFiyat = sipUrunAdet.Adet*firstOrDefault.Fiyat
                           };
                           siparislistesi.Add(siparis);
                       }
                       var serialSiparisler = new JavaScriptSerializer().Serialize(siparislistesi);
                       mesaj = sdb.SiparisOlustur(serialSiparisler);
                   }
                }
                return Json(new { sonuc = true, mesaj = mesaj });
            }
        }

        public bool  Kupon()
        {
            bool sonuc = false;
            using (ConDmnsServis.DmnsServisClient sdb = new DmnsServisClient())
            {
                if (Session["CurrentUser"]!=null)
                sonuc = sdb.Kupon(((Kisiler) Session["CurrentUser"]).KisiId);
            }
            return (sonuc);
        }

    }
}