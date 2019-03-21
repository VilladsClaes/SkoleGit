using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;


public class MailTool
{
    public static string SendMail(string MailFrom, string MailTo, string MailSubject, string MailBody, string Host, int port, string MyUserName, string MyPassword )
    {
        try
        {       
        //En instans af .net classen mailmessage (kend den på den grønne farve (kaldes et andet sted fra))
        // starter på en ny mail (laver en ny instans af mailmessage).. 
        //HUSK using (NameSpace eller bibliotek) Husk System.net.mail i toppen af controleren (intelesans tjek)
        MailMessage mail = new MailMessage();

        // From er = mailens afsender. Det er ikke mailens afsender når vi bruger gmails smtp, CHeck ved webhost.
        mail.From = new MailAddress(MailFrom);

        //Dette er den mail som man besvare til (tilbage til kd efter kontakt med hjemmeside staf(input MailFrom i Contact form))
        mail.ReplyToList.Add(MailFrom);

        //er den mail add der modtager mail fra form
        //statisk i Contact form, dynamisk i newlettter = mail from.
        mail.To.Add(MailTo);

        // Emnefelt på mail. VIGTIG, men ikke et ultimativt krav.
        mail.Subject = MailSubject;

        //Er den besked der skrives i textarea. (Da vi har valgt at sende HTML kan vi erstate NewLine(Enviroment) med <br/>)
        mail.Body = MailBody;
        mail.IsBodyHtml = true;

        

        //"smtp" er en instans af smtpClient,  Smtp = simpel mail transport protocol
        SmtpClient smtp = new SmtpClient();

        //host er udbyderen udgående mailserver.
        //her er der gmails smtp og port.
        //tjek med webhost........!
        smtp.Host = Host;
        smtp.Port = port;

        //ssl er nøjvendigt  for gmail - tjek med udbyder..
        smtp.EnableSsl = true;

        //Her slår vi standart longi-oplysningerne fra.. 
        smtp.UseDefaultCredentials = false;

        //her skriver vi vores login oplysninger.
        smtp.Credentials = new System.Net.NetworkCredential(MyUserName, MyPassword);

        //Her pakker vi hele instansen(alt data tastet ovenfor) "mail" ned som parameter til metoden send.
        smtp.Send(mail);
            return ("Mailen er sendt");
        }
        catch (Exception)
        {
            return ("Mailen er ikke sendt");
        }

    }
}
