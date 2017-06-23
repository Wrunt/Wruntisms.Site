namespace Wruntisms.Site.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using API;
    using Models;

    public class SongController : ApiController
    {
        // POST api/Song
        public void Post(SongViewModel vm)
        {
            return SongApi.CreateSong(vm);
        }

        // GET api/Song/5
        public SongViewModel Get(SongViewModel vm)
        {
            return SongApi.RetrieveSong(vm);
        }

        // PUT api/Song/5
        public SongViewModel Put(SongViewModel vm)
        {
            return SongApi.UpdateSong(vm);
        }

        // DELETE api/Song/5
        public SongViewModel Delete(SongViewModel vm)
        {
            return SongApi.DeleteSong(vm);
        }
    }
}