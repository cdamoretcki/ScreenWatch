using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Drawing;

namespace ScreenShotReceiver
{
    [ServiceContract]
    public interface IScreenShotReceiver
    {
        [OperationContract]
        void Upload(ImageUpload composite);
    }

    /// <summary>
    /// Image wrapping object.  also add any meta data here
    /// </summary>
    [DataContract]
    public class ImageUpload
    {
        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public Guid PCGuid { get; set; }

        [DataMember]
        public byte[] ImageData { get; set; }
    }
}
