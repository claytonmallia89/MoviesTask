import React, { useEffect, useState } from 'react';
import { MovieGrid } from '../../Components/MoviesGrid/MovieGrid';
import { getGenre, getMovies } from '../../Services/MovieService';
import { IGenre } from '../../Types/Genre';
import { IMovie } from '../../Types/Movie';
import { isEmpty } from '../../Utility/StringUtils';

function Home(){
    const [movieRows, setMovieRows] = useState<IMovie[]>([]);
    const [filteredMovies, setFilteredMovies] = useState<IMovie[]>([]);
    const [genreRows, setGenreRows] = useState<IGenre[]>([]);
    
useEffect(() => {
    getGenre().then(genre =>  {
        setGenreRows(genre);
    });

    filterMovieGrid(true, "", "");
  },[]);
    
  
    const filterMovieGrid = (isFirstRun:boolean, searchTitle:string, selectedGenre:string) =>{

        if(isFirstRun){
          getMovies().then(movieList => {
            setMovieRows(movieList);
            setFilteredMovies(movieList);
          });
          return;
        }
      
        var genreIdFilter:string  = "";
        var searchTitleFilter:string = "";
        let areSearchFiltersPresent:boolean = false;
        
        if(!isEmpty(searchTitle) && searchTitle.length > 2){
          areSearchFiltersPresent = true;
          searchTitleFilter= searchTitle;
        }
      
        if (!isEmpty( selectedGenre)){
            genreIdFilter = selectedGenre ?? "";
            areSearchFiltersPresent = true;
        }
          
        if(areSearchFiltersPresent){
          getMovies(searchTitleFilter,genreIdFilter ).then(movieList => {
            setFilteredMovies(movieList);
          });
        }else{  //else don't do a call to the backend and get the first list of rows
            setFilteredMovies(movieRows);
         }
      }      

    return (
        <>
            <MovieGrid 
                filterMovieGrid={ filterMovieGrid }
                genreList={genreRows}
                movieList={filteredMovies}
            />
        </>
    );
}

export default Home;