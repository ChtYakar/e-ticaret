using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServisler
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDmnsServis
    {

        [OperationContract]
        string Login(string email,string sifre);

        [OperationContract]
        string Urunler();

        [OperationContract]
        string UrunGetir(int urunId);

        [OperationContract]
        string SiparisOlustur(string siparisler);

        [OperationContract]
        bool Kupon(int kisiId);
        // TODO: Add your service operations here
    }


}
