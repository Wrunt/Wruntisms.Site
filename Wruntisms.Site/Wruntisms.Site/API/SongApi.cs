namespace Wruntisms.Site.API
{
    using System;
    using Models;

    public class SongApi
    {
        public static SongViewModel CreateSong(SongViewModel vm)
        {
            throw new NotImplementedException();

            return vm;
        }

        public static SongViewModel RetrieveSong(SongViewModel vm)
        {
            throw new NotImplementedException();

            //TODO: get from database
            //TODO: validate on youtube
            return vm;
        }

        public static SongViewModel UpdateSong(SongViewModel vm)
        {
            throw new NotImplementedException();

            //TODO: get from database
            //TODO: update in database
            return vm;
        }
        public static SongViewModel DeleteSong(SongViewModel vm)
        {
            throw new NotImplementedException();

            //TODO: get song from database
            //TODO: set song to deleted
            return vm;
        }
    }
}