import { IGenre } from "../Types/Genre";
import { IMovie } from "../Types/Movie";
import { ApiEnums } from "../Utility/ApiEndpoints";
import { get } from "../Utility/HttpUtils";
import { isEmpty } from "../Utility/StringUtils";

export function getGenre(): Promise<IGenre[]> {
  return get<IGenre[]>(ApiEnums.GENRES);
}

export function getMovies(searchQuery:string = "", genreId:string=""): Promise<IMovie[]> {
  const params = new URLSearchParams();      
  if(!isEmpty(searchQuery))
  {
    params.append('searchQuery', searchQuery);
  }

  if(!isEmpty(genreId))
  { 
    params.append('genreId', genreId.toString());
  }

  return get<IMovie[]>(ApiEnums.MOVIES,params);
}