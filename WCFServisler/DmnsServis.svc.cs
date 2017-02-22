using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace WCFServisler
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class DmnsServis : IDmnsServis
    {
        public string Login(string email, string sifre)
        {
            using (DmnsEntities db = new DmnsEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var kisi = db.Kisiler.FirstOrDefault(x => x.Eposta == email && x.Sifre == sifre);
                var jsonSerialize = new JavaScriptSerializer().Serialize(kisi);
                return jsonSerialize;
            }
        }


        public string Urunler()
        {
            using (DmnsEntities db= new DmnsEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var urunler = db.Urunler.ToList();
                var jsonSerialize = new JavaScriptSerializer().Serialize(urunler);
                return jsonSerialize;
            }
        }


        public string UrunGetir(int urunId)
        {
            using (DmnsEntities db = new DmnsEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var urunler = db.Urunler.FirstOrDefault(x => x.UrunId == urunId);
                var jsonSerialize = new JavaScriptSerializer().Serialize(urunler);
                return jsonSerialize;
            }
        }


        public string SiparisOlustur(string siparisler)
        {
            using (DmnsEntities db = new DmnsEntities())
            {
                List<Siparisler> _siparisler = new JavaScriptSerializer().Deserialize<List<Siparisler>>(siparisler);
                foreach (var _siparis in _siparisler)
                {
                    Siparisler _sprs = new Siparisler()
                    {
                        Quantity = _siparis.Quantity,
                        KisiId = _siparis.KisiId,
                        UrunId = _siparis.UrunId,
                        ToplamFiyat = _siparis.ToplamFiyat,
                       
                    };
                    db.Siparisler.Add(_sprs);
                    
                }
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        return "Siparişler kaydedildi";
                    }
                }
                catch (Exception ex)
                {
                        
                    throw new Exception(ex.ToString());
                }
                

            }
            return "Sipariş alınırken hata oluştu";
        }


        public bool Kupon(int kisiId)
        {
            using (DmnsEntities db = new DmnsEntities())
            {
                var eskiSip = db.Siparisler.FirstOrDefault(x => x.KisiId == kisiId);
                return (eskiSip == null);
            }
            
        }
    }

}
