﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos12.Entities
{
    public class Jogo
    {
        public Guid Id { get; set; }
        public string Nome { get; set;}
        public string Produtora { get; set; }
        public double Preço { get; set; }
    }
}
