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
        if (semestrNazwa.Substring(0, 6) == "zimowy")
        {
            if (semestrNazwa.Length != 10 || !Char.IsDigit(semestrNazwa[6]) || !Char.IsDigit(semestrNazwa[7])
                                          || !Char.IsDigit(semestrNazwa[8]) || !Char.IsDigit(semestrNazwa[9]))
            {
                throw new ArgumentException("Nieprawidłowa format nazwy pliku: " + filePath);
            }
            semestrNazwa = "zimowy (20" + semestrNazwa.Substring(6, 2) + "/20"
                            + semestrNazwa.Substring(8, 2) + ")";
        }
        else if (semestrNazwa.Substring(0, 5) == "letni")
        {
            if (semestrNazwa.Length != 9 || !Char.IsDigit(semestrNazwa[5]) || !Char.IsDigit(semestrNazwa[6])
                                         || !Char.IsDigit(semestrNazwa[7]) || !Char.IsDigit(semestrNazwa[8]))
            {
                throw new ArgumentException("Nieprawidłowa format nazwy pliku: " + filePath);
            }
            semestrNazwa = "letni (20" + semestrNazwa.Substring(5, 2) + "/20"
                            + semestrNazwa.Substring(7, 2) + ")";
        }
        else
        {
            throw new ArgumentException("Nieprawidłowa format nazwy pliku: " + filePath);
        }

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

            if (nazwy_tagow.Length != skr_nazwy_tagow.Length)
            {
                throw new ArgumentException("Liczba nazw tagów i skrótów tagów nie jest taka sama w pliku: " + filePath);
            }

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

            if (dane[4] != "t" && dane[4] != "f")
            {
                throw new ArgumentException("Nieprawidłowa wartość dla 'czyIrok' w pliku: " + filePath);
            }

            if (!int.TryParse(dane[1], out int ects) || ects < 0)
            {
                throw new ArgumentException("Nieprawidłowa wartość ECTS w pliku: " + filePath);
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