class Filtry
{
    public string nazwa { get; set; }
    public List<Rodzaj> rodzaje { get; set; }
    public List<Tag> tagi { get; set; }
    public List<Semestr> semestry { get; set; }
    public bool czyIrok { get; set; }
}

class Filtrowanie
{
    public static List<Przedmiot> FiltrujPrzedmioty(List<Przedmiot> przedmioty, Filtry filtry)
    {
        var przefiltrowane = przedmioty.AsQueryable();
        if (!string.IsNullOrEmpty(filtry.nazwa))
        {
            przefiltrowane = przefiltrowane.Where(p => p.nazwa.Contains(filtry.nazwa, StringComparison.OrdinalIgnoreCase));
        }
        if (filtry.rodzaje != null && filtry.rodzaje.Count > 0)
        {
            przefiltrowane = przefiltrowane.Where(p => filtry.rodzaje.Contains(p.rodzaj));
        }
        if (filtry.tagi != null && filtry.tagi.Count > 0)
        {
            przefiltrowane = przefiltrowane.Where(p => p.tagi.Any(t => filtry.tagi.Contains(t)));
        }
        if (filtry.semestry != null && filtry.semestry.Count > 0)
        {
            przefiltrowane = przefiltrowane.Where(p => filtry.semestry.Contains(p.semestr));
        }
        if (filtry.czyIrok)
        {
            przefiltrowane = przefiltrowane.Where(p => p.czyIrok);
        }
        return przefiltrowane.ToList();
    }
}