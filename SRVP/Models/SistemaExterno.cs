﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.Models
{
    public class SistemaExterno
    {
        [Key, Required]
        public Guid id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string secreto { get; set; }
        //public string cuit { get; set; }    //Dudoso el tema cuit
        [Required]                               //lo pidio el profe, no se para que
        public string paginaRetorno { get; set; }
        //public string publicoId { get; set; } redundante
        //ok, lo borramos?
    }
}
