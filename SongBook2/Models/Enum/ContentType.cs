using System;
namespace SongBook2.Models
{
    public enum ContentType
    {
        Simple,//string simples
        Html,//a string salva no banco é um html         
        FromUrl //o conteudo é um iframe
    }
}
