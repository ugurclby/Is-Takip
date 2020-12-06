﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class Gorev : ITablo
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturulmaTarih { get; set; } = DateTime.Now;

        public int? AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public int AciliyetId { get; set; }
        public Aciliyet Aciliyet { get; set; }

        public int RaporId { get; set; }
        public List<Rapor> Raporlar { get; set; }
    }
}
