import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Box, Button } from '@mui/material';

import Home from "./pages/Home/Home";
import Autores from "./pages/Autores/Autores";
import Generos from './pages/Generos/Generos';
import Livros from './pages/Livros/Livros';

function App() {
  return (
    <Router>
      <Box sx={{ flexGrow: 1 }}>
        {/* Menu superior */}
        <AppBar position="static">
          <Toolbar>
            <Typography variant="h6" sx={{ flexGrow: 1 }}>
              Gestão de Biblioteca
            </Typography>

            {/* Menu direito */}
            <Button color="inherit" component={Link} to="/home">Início</Button>
            <Button color="inherit" component={Link} to="/autores">Autores</Button>
            <Button color="inherit" component={Link} to="/generos">Gêneros</Button>
            <Button color="inherit" component={Link} to="/livros">Livros</Button>
          </Toolbar>
        </AppBar>

        {/* Conteúdo principal */}
        <Box sx={{ padding: 3 }}>
          <Routes>
            {/* Home em branco */}
            <Route path="/" element={<Home />} />

            {/* Página de Autores com CRUD */}
            <Route path="/home" element={<Home />} />
            <Route path="/autores" element={<Autores />} />
            <Route path="/generos" element={<Generos />} />
            <Route path="/livros" element={<Livros />} />
          </Routes>
        </Box>
      </Box>
    </Router>
  );
}

export default App;