namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoomFeature
    {
        public int RoomFeatureID { get; set; }

        public int RoomID { get; set; }

        public int FeatureID { get; set; }

        public virtual Feature Feature { get; set; }

        public virtual Room Room { get; set; }
    }
}
