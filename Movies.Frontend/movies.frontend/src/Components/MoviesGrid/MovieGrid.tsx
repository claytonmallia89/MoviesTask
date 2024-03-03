import { FunctionComponent, PropsWithChildren, useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import SearchBar from "material-ui-search-bar";
import MenuItem from '@mui/material/MenuItem';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import Stack from "@mui/material/Stack/Stack";
import Chip from "@mui/material/Chip";
import FaceIcon from '@mui/icons-material/Face';
import { IMovie } from "../../Types/Movie";
import { IGenre } from "../../Types/Genre";
import FormControl from "@material-ui/core/FormControl";

//Alternatively i could have uses scss and then reference it
const useComponentStyles = makeStyles({
  paper: {
    width: '98%',
    overflow: 'hidden' 
  }, 

  table: {
    minWidth: 650,
    marginTop:10,
    border:"1px solid"
  },

  tableRow30: {
    width:'30%'
  },
  tableRow15: {
    width: '15%'
  },
  tableRow10: {
    width: '10%'
  }
});

export interface IMovieGridProps {
   filterMovieGrid : (isFirstRun:boolean, searchTitle:string, selectedGenre:string) => void;
   genreList:IGenre[];
   movieList:IMovie[]
}

export const MovieGrid: FunctionComponent<PropsWithChildren<IMovieGridProps>> = ({filterMovieGrid,genreList,movieList}) => {
  const [filteredMovies, setFilteredMovies] = useState<IMovie[]>([]);
  const [genreRows, setGenreRows] = useState<IGenre[]>([]);
  const [selectedGenre, setSelectedGenre] = useState<string>("");
  const [searchTitle, setSearchTitle] = useState<string>("");
  const componentStyling = useComponentStyles();

  useEffect(() => {
    setGenreRows(genreList);
    setFilteredMovies(movieList)
  }, [genreList, movieList]);
    
  useEffect(() => {
    filterMovieGrid(false, searchTitle, selectedGenre);
  }, [searchTitle, selectedGenre])


const requestSearch = (searchTitle: string) => {
  setSearchTitle(searchTitle);
};

const cancelSearch = () => {
    setSearchTitle("");
    requestSearch(searchTitle);
};

const handleGenreChange = (event: SelectChangeEvent) => {
  if(event.target.value  == null ){
    setSelectedGenre("");
  }else{
    setSelectedGenre(event.target.value.toString());
  }
};
return (
  <>         
    <Paper className={componentStyling.paper}>     
      <SearchBar
        value={searchTitle}
        onChange={(searchValue:string) => requestSearch(searchValue)}
        onCancelSearch={() => cancelSearch}
	      placeholder="Search for title"
      />
	
      <FormControl fullWidth>
        <Select
          labelId="select-genre-label"
          id="genre-select"
          value={selectedGenre}
          label="Select Genre"
          displayEmpty
         // inputProps={{ 'aria-label': 'Without label' }}
          onChange={(handleGenreChange)}
        >
          <MenuItem value="">
              <em>Select Genre</em>
            </MenuItem>
          {genreRows.map((value,index)=> 
            <MenuItem value={value.id} key={`genre-${value.id}`}>{value.name}</MenuItem>
          )}
        </Select>
      </FormControl>
      <TableContainer>
        <Table className={componentStyling.table} aria-label="Movies">
          <TableHead>
            <TableRow>
              <TableCell className={componentStyling.tableRow30} >Title</TableCell>
              <TableCell className={componentStyling.tableRow15} align="right">Year</TableCell>        
              <TableCell className={componentStyling.tableRow15} align="center">Actors</TableCell>        
              <TableCell className={componentStyling.tableRow15} align="right">Director</TableCell>               
              <TableCell className={componentStyling.tableRow15} align="right">Rating</TableCell>
              <TableCell className={componentStyling.tableRow10} align="center">Genre</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {
              (filteredMovies.length > 0) &&  (                                
                filteredMovies.map((movie, index) => (
                  <TableRow key={`movie-${index}`}>
                    <TableCell component="th" scope="row">{movie.title}</TableCell>
                    <TableCell align="right">{movie.year}</TableCell>
                    <TableCell align="right">
                      <Stack direction="row" spacing={1}>
                        {movie.actors.map((actor, index) =>
                          <Chip icon={<FaceIcon />} label={actor} size="small" />
                        )}							
                      </Stack>
                    </TableCell>
                    <TableCell align="right">{movie.director}</TableCell>
                    <TableCell align="right">{movie.rating}</TableCell>
                    <TableCell align="right">
                      <Stack direction="row" spacing={1}>
                        {movie.genre.map((genre, index) =>
                          <Chip label={genre} size="small" />
                        )}	
                      </Stack>
                    </TableCell>
                  </TableRow>
                )))                    
            }
            {
              (filteredMovies.length === 0) && (
                <TableRow>
                  <TableCell colSpan={6}>No data to show</TableCell>
                </TableRow>
              )
            }
          </TableBody>
        </Table>
      </TableContainer>
    </Paper>
  </>
  );
}