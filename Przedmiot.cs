class Rodzaj
{
    public string nazwa { get; set; }
    public Rodzaj(string nazwa)
    {
        this.nazwa = nazwa;
    }
}

class Tag
{
    public string nazwa { get; set; }
    public string skr_nazwa { get; set; }
    public Tag(string nazwa, string skr_nazwa)
    {
        this.nazwa = nazwa;
        this.skr_nazwa = skr_nazwa;
    }
}

class Semestr
{
    public string nazwa { get; set; }
    public Semestr(string nazwa)
    {
        this.nazwa = nazwa;
    }
}

class Przedmiot
{
    public string nazwa { get; set; }
    public Rodzaj rodzaj { get; set; }
    public int ects { get; set; }
    public bool czyIrok { get; set; }
    public Tag[] tagi { get; set; }
    public Semestr semestr { get; set; }
    public Przedmiot(string nazwa, Rodzaj rodzaj, int ects, bool czyIrok, Tag[] tagi, Semestr semestr)
    {
        this.nazwa = nazwa;
        this.rodzaj = rodzaj;
        this.ects = ects;
        this.czyIrok = czyIrok;
        this.tagi = tagi;
        this.semestr = semestr;
    }
}