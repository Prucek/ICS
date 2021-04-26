using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.App.Messages
{
    // bázová trieda pre všetky typy správ, ktoré môžu View Modely posielať
    // v jednotlivých triedach v tomto priečinku sú tieto správy
    // každá sa posiela pri špecifickej udalosti...SelectedMessage napr. keď uživateľ
    // klikne na nejakú položku vo View

    public abstract class Message<T> : IMessage
        where T : IModel
    {
        private Guid? _id;

        public Guid Id
        {
            get => _id ?? Model.Id;
            set => _id = value;
        }

        public Guid TargetId { get; set; }
        public T Model { get; set; }
    }
}
