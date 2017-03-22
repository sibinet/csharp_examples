using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace send_appointment
{
    class Program
    {
        static void Main(string[] args)
        {

            //Configure Email
            MailMessage mailmsg = new MailMessage();
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            mailmsg.From = new MailAddress("yourorganizername@gmail.com", "Event Orgizer");
            sc.Credentials = new NetworkCredential("yourorganizername@gmail.com", "password");
            sc.EnableSsl = true;

            //attendee Email
            mailmsg.To.Add(new MailAddress("yourattendee@yourdomain.com", "attendeename"));
           
            mailmsg.Subject = "New Task from C# Application";
            mailmsg.Body = "Here is some exmaple email from C#";

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//GeO");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");

            //Eevent Details
            //Start Date, End Date, Location etc.
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", DateTime.Today.AddDays(1)));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", DateTime.Today.AddDays(1).AddHours(10)));
            str.AppendLine("LOCATION: " + "Your Place");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION;ENCODING=QUOTED-PRINTABLE:{0}", mailmsg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", mailmsg.Body));
            str.AppendLine(string.Format("SUMMARY;ENCODING=QUOTED-PRINTABLE:{0}", mailmsg.Subject));

            //Organizer and attendee
            //NOTE: If oragnizer and attendee is same, Event will not appear

            str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", mailmsg.From.Address));
            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", mailmsg.To[0].DisplayName, mailmsg.To[0].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION;ENCODING=QUOTED-PRINTABLE:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            //Attach Event
            System.Net.Mime.ContentType type = new System.Net.Mime.ContentType("text/calendar");
            type.Parameters.Add("method", "REQUEST");
            type.Parameters.Add("name", "Event.ics");
            mailmsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(str.ToString(), type));


            sc.Send(mailmsg);
        }
    }
}
