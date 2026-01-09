using Microsoft.AspNetCore.Mvc.RazorPages;
using auto.Data;
using auto.Models;

namespace auto.Models
{
    public class ProgramareServiciiPageModel : PageModel
    {
        public List<AssignedServiciuData> AssignedServiciuDataList;

        public void PopulateAssignedServiciuData(autoContext context, Programare programare)
        {
            var allServicii = context.Serviciu;
            var programareServicii = new HashSet<int>(
                programare.ProgramariServicii.Select(c => c.ServiciuID));

            AssignedServiciuDataList = new List<AssignedServiciuData>();
            foreach (var serv in allServicii)
            {
                AssignedServiciuDataList.Add(new AssignedServiciuData
                {
                    ServiciuID = serv.ID,
                    Nume = serv.Denumire,
                    Pret = serv.Pret,
                    Assigned = programareServicii.Contains(serv.ID)
                });
            }
        }

        public void UpdateProgramareServicii(autoContext context, string[] selectedServicii, Programare programareToUpdate)
        {
            if (selectedServicii == null)
            {
                programareToUpdate.ProgramariServicii = new List<ProgramareServiciu>();
                return;
            }

            var selectedServiciiHS = new HashSet<string>(selectedServicii);
            var programareServicii = new HashSet<int>(programareToUpdate.ProgramariServicii.Select(c => c.Serviciu.ID));

            foreach (var serv in context.Serviciu)
            {
                if (selectedServiciiHS.Contains(serv.ID.ToString()))
                {
                    if (!programareServicii.Contains(serv.ID))
                    {
                        programareToUpdate.ProgramariServicii.Add(new ProgramareServiciu
                        {
                            ProgramareID = programareToUpdate.ID,
                            ServiciuID = serv.ID
                        });
                    }
                }
                else
                {
                    if (programareServicii.Contains(serv.ID))
                    {
                        ProgramareServiciu serviciuToRemove = programareToUpdate.ProgramariServicii
                            .SingleOrDefault(i => i.ServiciuID == serv.ID);
                        context.Remove(serviciuToRemove);
                    }
                }
            }
        }
    }
}