using Maurizio_Casu_Test;

bool exit = false;
do
{
    Console.WriteLine("Benvenuti al programma di gestione spese.\r\n" +
        "Inserire l'operazione desiderata tra le seguenti:\r\n" +
        "1.Inserire nuova Spesa\r\n" +
        "2.Approvare una spesa esistente\r\n" +
        "3.Cancellare una spesa esistente\r\n" +
        "4.Mostrare l'elenco delle spese approvate\r\n" +
        "5.Mostrare l'elenco delle spese di uno specifico Utente\r\n" +
        "6.Mostrare il totale delle spese per categoria\r\n" +
        "PREMERE QUALSIASI ALTRO TASTO PER USCIRE");
     string scelta = Console.ReadLine();
    switch (scelta)
    {
        case "1":
            {
                AdoNet.InsertSpesa();
                break;
            }
        case "2":
            {
                AdoNet.ApproveSpesa();
                break;
            }
        case "3":
            {
                AdoNet.DeleteSpesa();
                break;
            }
        case "4":
            {
                AdoNet.ShowApprovedSpesa();
                break;
            }
        case "5":
            {
                AdoNet.ShowUserSpesa();
                break;
            }
        case "6":
            {
                AdoNet.ShowCategorySpesa();
                break;
            }
        default:
            {
                exit=true;
                break;
            }
        

    }

}while(!exit);