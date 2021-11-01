using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Models.Component
{
    public class Dict : BindableBase
    {
        public Dict()
        {
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string word;

        public string Word
        {
            get { return word; }
            set { SetProperty(ref word, value); }
        }

        private string phonetic;

        public string Phonetic
        {
            get { return phonetic; }
            set { SetProperty(ref phonetic, value); }
        }

        private string definition;

        public string Definition
        {
            get { return definition; }
            set { SetProperty(ref definition, value); }
        }

        private string translation;

        public string Translation
        {
            get { return translation; }
            set { SetProperty(ref translation, value); }
        }

        private string pos;

        public string Pos
        {
            get { return pos; }
            set { SetProperty(ref pos, value); }
        }

        private string collins;

        public string Collins
        {
            get { return collins; }
            set { SetProperty(ref collins, value); }
        }

        private string oxford;

        public string Oxford
        {
            get { return oxford; }
            set { SetProperty(ref oxford, value); }
        }

        private string tag;

        public string Tag
        {
            get { return tag; }
            set { SetProperty(ref tag, value); }
        }

        private string bnc;

        public string Bnc
        {
            get { return bnc; }
            set { SetProperty(ref bnc, value); }
        }

        private string frq;

        public string Frq
        {
            get { return frq; }
            set { SetProperty(ref frq, value); }
        }

        private string exchange;

        public string Exchange
        {
            get { return exchange; }
            set { SetProperty(ref exchange, value); }
        }

        private string detail;

        public string Detail
        {
            get { return detail; }
            set { SetProperty(ref detail, value); }
        }

        private string audio;

        public string Audio
        {
            get { return audio; }
            set { SetProperty(ref audio, value); }
        }
    }
}