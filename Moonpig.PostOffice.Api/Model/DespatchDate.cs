namespace Moonpig.PostOffice.Api.Model
{
    using System;

    public class DespatchDate
    {
        public DateTime Date { get; set; }

        public string Error { get; set; } = "no error";
    }
}