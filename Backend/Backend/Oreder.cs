//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Backend
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oreder
    {
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public System.DateTime Date { get; set; }
        public long Client_ClientId { get; set; }
        public long Status_StatusID { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Status Status { get; set; }
    }
}
