using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course
{
    public class Translit
    {
        // объявляем и заполняем словарь с заменами
        // при желании можно исправить словать или дополнить
        Dictionary<string, string> dictionaryChar = new Dictionary<string, string>()
            {
                {"а","a"},
                {"б","b"},
                {"в","v"},
                {"г","g"},
                {"д","d"},
                {"е","e"},
                {"ё","yo"},
                {"ж","zh"},
                {"з","z"},
                {"и","i"},
                {"й","y"},
                {"к","k"},
                {"л","l"},
                {"м","m"},
                {"н","n"},
                {"о","o"},
                {"п","p"},
                {"р","r"},
                {"с","s"},
                {"т","t"},
                {"у","u"},
                {"ф","f"},
                {"х","h"},
                {"ц","ts"},
                {"ч","ch"},
                {"ш","sh"},
                {"щ","sch"},
                {"ъ","'"},
                {"ы","yi"},
                {"ь",""},
                {"э","e"},
                {"ю","yu"},
                {"я","ya"},
                {"А", "A"},
                {"Б", "B"},
                {"В", "V"},
                {"Г", "G"},
                {"Д", "D"},
                {"Е", "E"},
                {"Ё", "Yo"},
                {"Ж", "Zh"},
                {"З", "Z"},
                {"И", "I"},
                {"Й", "Y"},
                {"К", "K"},
                {"Л", "L"},
                {"М", "M"},
                {"Н", "N"},
                {"О", "O"},
                {"П", "P"},
                {"Р", "R"},
                {"С", "S"},
                {"Т", "T"},
                {"У", "U"},
                {"Ф", "F"},
                {"Х", "H"},
                {"Ц", "Ts"},
                {"Ч", "Ch"},
                {"Ш", "Sh"},
                {"Щ", "Sch"},
                {"Ъ", "'"},
                {"Ы", "Yi"},
                {"Ь", ""},
                {"Э", "E"},
                {"Ю", "Yu"},
                {"Я", "Ya"}
            };
        /// <summary>
        /// метод делает транслит на латиницу
        /// </summary>
        /// <param name="source"> это входная строка для транслитерации </param>
        /// <returns>получаем строку после транслитерации</returns>
        public string TranslitFileName(string source)
        {
            var result = "";
            // проход по строке для поиска символов подлежащих замене которые находятся в словаре dictionaryChar
            foreach (var ch in source)
            {
                var ss = "";
                // берём каждый символ строки и проверяем его на нахождение его в словаре для замены,
                // если в словаре есть ключ с таким значением то получаем true 
                // и добавляем значение из словаря соответствующее ключу
                if (dictionaryChar.TryGetValue(ch.ToString(), out ss))
                {
                    result += ss;
                }
                // иначе добавляем тот же символ
                else result += ch;
            }
            return result;
        }
    }
    public class GenreTrenslit
    {
        Dictionary<string, string> dictionaryGenre = new Dictionary<string, string>() 
        {
            {"sf_history", "Альтернативная история"},
            {"sf_action", "Боевая фантастика"},
            {"sf_epic", "Эпическая фантастика"},
            {"sf_heroic", "Героическая фантастика"},
            {"sf_detective", "Детективная фантастика"},
            {"sf_cyberpunk", "Киберпанк"},
            {"sf_space", "Космическая фантастика"},
            {"sf_social", "Социально-психологическая фантастика"},
            {"sf_horror", "Ужасы и Мистика"},
            {"sf_humor", "Юмористическая фантастика"},
            {"sf_fantasy", "Фэнтези"},
            {"sf", "Научная Фантастика"},
            {"det_classic", "Классический детектив"},
            {"det_police", "Полицейский детектив"},
            {"det_action", "Боевик"},
            {"det_irony", "Иронический детектив"},
            {"det_history", "Исторический детектив"},
            {"det_espionage", "Шпионский детектив"},
            {"det_crime", "Криминальный детектив"},
            {"det_political", "Политический детектив"},
            {"det_maniac", "Маньяки"},
            {"det_hard", "Крутой детектив"},
            {"thriller", "Триллер"},
            {"detective", "Детектив"},
            {"prose_classic", "Классическая проза"},
            {"prose_history", "Историческая проза"},
            {"prose_contemporary", "Современная проза"},
            {"prose_counter", "Контркультура"},
            {"prose_rus_classic", "Русская классическая проза"},
            {"prose_su_classics", "Советская классическая проза"},
            {"love_contemporary", "Современные любовные романы"},
            {"love_history", "Исторические любовные романы"},
            {"love_detective", "Остросюжетные любовные романы"},
            {"love_short", "Короткие любовные романы"},
            {"love_erotica", "Эротика"},
            {"adv_western", "Вестерн"},
            {"adv_history", "Исторические приключения"},
            {"adv_indian", "Приключения про индейцев"},
            {"adv_maritime", "Морские приключения"},
            {"adv_geo", "Путешествия и география"},
            {"adv_animal", "Природа и животные"},
            {"adventure", "Прочие приключения"},
            {"child_tale", "Сказка"},
            {"child_verse", "Детские стихи"},
            {"child_prose", "Детскиая проза"},
            {"child_sf", "Детская фантастика"},
            {"child_det", "Детские остросюжетные"},
            {"child_adv", "Детские приключения"},
            {"child_education", "Детская образовательная литература"},
            {"children", "Прочая детская литература"},
            {"poetry", "Поэзия"},
            {"dramaturgy", "Драматургия"},
            {"antique_ant", "Античная литература"},
            {"antique_european", "Европейская старинная литература"},
            {"antique_russian", "Древнерусская литература"},
            {"antique_east", "Древневосточная литература"},
            {"antique_myths", "Эпос"},
            {"antique", "Прочая старинная литература"},
            {"sci_history", "История"},
            {"sci_psychology", "Психология"},
            {"sci_culture", "Культурология"},
            {"sci_religion", "Религиоведение"},
            {"sci_philosophy", "Философия"},
            {"sci_politics", "Политика"},
            {"sci_business", "Деловая литература"},
            {"sci_juris", "Юриспруденция"},
            {"sci_linguistic", "Языкознание"},
            {"sci_medicine", "Медицина"},
            {"sci_phys", "Физика"},
            {"sci_math", "Математика"},
            {"sci_chem", "Химия"},
            {"sci_biology", "Биология"},
            {"sci_tech", "Технические науки"},
            {"science", "Прочая научная литература"},
            {"comp_www", "Интернет"},
            {"comp_programming", "Программирование"},
            {"comp_hard", "Компьютерное железо"},
            {"comp_soft", "Программы"},
            {"comp_db", "Базы данных"},
            {"comp_osnet", "ОС и Сети"},
            {"computers", "Прочая околокомпьтерная литература"},
            {"ref_encyc", "Энциклопедии"},
            {"ref_dict", "Словари"},
            {"ref_ref", "Справочники" },
            {"ref_guide", "Руководства"},
            {"reference", "Прочая справочная литература"},
            {"nonf_biography", "Биографии"},
            {"nonf_publicism", "Публицистика"},
            {"nonf_criticism", "Критика"},
            {"design", "Искусство и Дизайн"},
            {"nonfiction", "Прочая документальная литература"},
            {"religion_rel", "Религия"},
            {"religion_esoterics", "Эзотерика"},
            {"religion_self", "Самосовершенствование"},
            {"religion", "Прочая религионая литература"},
            {"humor_anecdote", "Анекдоты"},
            {"humor_prose", "Юмористическая проза"},
            {"humor_verse", "Юмористические стихи"},
            {"humor", "Прочий юмор"},
            {"home_cooking", "Кулинария"},
            {"home_pets", "Домашние животные"},
            {"home_crafts", "Хобби и ремесла"},
            {"home_entertain", "Развлечения"},
            {"home_health", "Здоровье" },
            {"home_garden", "Сад и огород"},
            {"home_diy", "Сделай сам"},
            {"home_sport", "Спорт"},
            {"home_sex", "Эротика"},
            {"home", "Прочиее домоводство"}
        };
        /// <summary>
        /// метод делает транслит обозначения жанров на норальное обозначение
        /// </summary>
        /// <param name="source"> это входная строка для транслитерации </param>
        /// <returns>получаем строку после транслитерации</returns>
        public string TranslitGenre(string source)
        {
            var result = "";
            if (dictionaryGenre.TryGetValue(source.ToString(), out string ss))
            {
                result = ss;
            }
            else result = source;

            return result;
        }
    }
}
