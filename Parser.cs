class Parser
{
    private Dictionary<string, Semestr> semestry = new Dictionary<string, Semestr>();
    private Dictionary<string, Rodzaj> rodzaje = new Dictionary<string, Rodzaj>();
    private Dictionary<string, Tag> tagi = new Dictionary<string, Tag>();

    public List<Przedmiot> parser(string filePath)
    {
        List<Przedmiot> przedmioty = new List<Przedmiot>();
        string[] linie = File.ReadAllLines(filePath);

        string semestrNazwa = Path.GetFileNameWithoutExtension(filePath);
        if (semestrNazwa[0] == 'z')
            semestrNazwa = "zimowy (20" + semestrNazwa[6] + semestrNazwa[7]
                                + "/20" + semestrNazwa[8] + semestrNazwa[9] + ")";
        else semestrNazwa = "letni (20" + semestrNazwa[5] + semestrNazwa[6]
                                + "/20" + semestrNazwa[7] + semestrNazwa[8] + ")";

        if (!semestry.ContainsKey(semestrNazwa))
        {
            semestry[semestrNazwa] = new Semestr(semestrNazwa);
        }
        Semestr semestr = semestry[semestrNazwa];

        foreach (string linia in linie.Skip(2))
        {
            string[] dane = linia.Split('|')
                                 .Select(d => d.Trim())
                                 .ToArray();
            if (dane.Length != 7) continue;

            if (!rodzaje.ContainsKey(dane[2]))
            {
                rodzaje[dane[2]] = new Rodzaj(dane[2]);
            }
            Rodzaj rodzaj = rodzaje[dane[2]];

            string[] nazwy_tagow = dane[6].Trim('{', '}')
                                          .Split(',')
                                          .Where(tag => !string.IsNullOrEmpty(tag))
                                          .Select(tag => tag.Trim())
                                          .ToArray();

            string[] skr_nazwy_tagow = dane[5].Trim('{', '}')
                                              .Split(',')
                                              .Where(tag => !string.IsNullOrEmpty(tag))
                                              .Select(tag => tag.Trim())
                                              .ToArray();

            List<Tag> tagiList = new List<Tag>();
            for (int i = 0; i < nazwy_tagow.Length; i++)
            {
                string tagKey = $"{nazwy_tagow[i]}|{skr_nazwy_tagow[i]}";
                if (!tagi.ContainsKey(tagKey))
                {
                    tagi[tagKey] = new Tag(nazwy_tagow[i], skr_nazwy_tagow[i]);
                }
                tagiList.Add(tagi[tagKey]);
            }

            Przedmiot przedmiot = new Przedmiot
            (
                dane[0],
                rodzaj,
                int.Parse(dane[1]),
                dane[4] == "t",
                tagiList.ToArray(),
                semestr
            );

            przedmioty.Add(przedmiot);
        }

        return przedmioty;
    }
    public List<Rodzaj> getRodzaje()
    {
        return rodzaje.Values.ToList();
    }
    public List<Tag> getTagi()
    {
        return tagi.Values.ToList();
    }
    public List<Semestr> getSemestry()
    {
        return semestry.Values.ToList();
    }
}