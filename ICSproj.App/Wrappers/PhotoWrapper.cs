using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.App.Wrappers
{
    // Wrapper je niečo ako medzivrstva medzi modelmi z BL a View modelmi z tejto fazy
    // Zabezpečuje mapovanie medzi týmito dvoma typmi modelov
    // Uľahčuje View modelom jednoduchšie prepisovať to čo vidí užívateľ

    public class PhotoWrapper : ModelWrapper<PhotoDetailModel>
    {
        public PhotoWrapper(PhotoDetailModel model)
            : base(model)
        { }

        public byte[] Photo
        {
            get => GetValue<byte[]>();
            set => SetValue(value);
        }

        public Guid ForeignGuid
        {
            get => GetValue<Guid>(); 
            set => SetValue(value);
        }

        public static implicit operator PhotoWrapper(PhotoDetailModel detailModel)
            => new(detailModel);

        public static implicit operator PhotoDetailModel(PhotoWrapper wrapper)
            => wrapper.Model;
    }
}
