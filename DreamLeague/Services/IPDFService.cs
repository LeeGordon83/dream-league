using DreamLeague.Inputs;
using IronPdf;

namespace DreamLeague.Services
{
    public interface IPDFService
    {
        void SealedBidBuilder(TeamSheet teamSheet);
    }
}