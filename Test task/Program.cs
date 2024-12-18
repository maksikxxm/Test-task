namespace Test_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Универсальную утилита в моем представлении кратко выглядит так. 
            //Программа на вход получает два коннекта и названия табличек.
            //Далее получает DDL каждой из них и производит сравнение. По результатам сравнения либо переносит данные полностью, либо частично, либо вообще не переносит.
            //Что при каких условиях будет сделано, надо уже определять конкретно.
            //Например, если целевой таблицы нет, то мы можем ее создать и продолжить перенос, или ничгео не делать. Возможно, сделать перенос и табличек, которые имеют связи с исходной табличкой.
            //Надо рассматривать много крайних случаев.
            //Так же как вариант можно сделать перенос данных через файл, это будет работать быстрее, но накладывается много ограничений.
            //Вообще, исходя из постановки задачи, простор для фантазии тут большой.
            string db = "db";
            string db1 = "db1";

            string dbRecipe = "db.recipe";

            //Добавить логгирование
            string[] dbRecipeColums = GetTableColumsName("db.recipe");

            string db1Recipe = "db1.recipe";
            string[] db1RecipeColums = GetTableColumsName("db.recipe");

            //Представим, что переносим только данные по повторяющимся столцам, а остальные игнорируем
            string[] intersectColums = (
                                       from colum in dbRecipeColums
                                       where db1RecipeColums.Contains(colum)
                                       select colum
                                       ).ToArray();

            object data = GetData(dbRecipe, intersectColums);
            SetData(dbRecipe, intersectColums, data);
        }

        static string[] GetTableColumsName(string table)
        {
            //Представим, что выполняем запрос к бд и получаем всю необходимую информацию о таблице(название колонок, типы, констрейнты и т.д и т.п)
            //Остановимся просто на названии колонок
            switch (table)
            {
                case "db.recipe":
                    return new string[] { "id", "name", "date_modified", "mixer_set_id", "time_set_id" };
                case "db.recipe_mixer_set": 
                    return new string[] { "id", "name", "unload_time", "upload_mode" };
                case "db.recipe_time_set":
                    return new string[] { "id", "name", "mix_time" };
                case "db.component_type":
                    return new string[] { "id", "type" };
                case "db.component":
                    return new string[] { "id", "name", "type_id", "humidity" };
                case "db.recipe_structure":
                    return new string[] { "recipe_id", "component_id", "amount", "correct_value" };
                case "db1.recipe":
                    return new string[] { "id", "name", "date_modified", "mix_time", "mixer_humidity", "water_correct" };
                case "db1.component_type":
                    return new string[] { "id", "type" };
                case "db1.recipe_structure":
                    return new string[] { "recipe_id ", "component_id", "amount" };
            }

            return new string[] { };
        }

        static object GetData(string table, string[] colums)
        {
            //Представим, что тут запрос в бд, которые делает селект из таблички по столбцам
            return table.Concat(".data");
        }

        static void SetData(string table, string[] colums, object data /*можно тут передать какой-нибудь логгер и использовать его*/)
        {
            //Представим, что тут запрос в бд, которые делает инсерт
            try
            {
                //while
                //{
                //  ...
                //  Concole.WriteLine("Запись с таким-то ид добавлена успешно")
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);//логгируем
            }
        }
    }
}
