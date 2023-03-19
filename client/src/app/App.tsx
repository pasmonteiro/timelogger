import * as React from "react";
import Projects from "./views/Projects";
import Project from "./views/Project";
import { ToastContainer } from 'react-toastify';
import { Routes, Route } from 'react-router-dom';
import "./style.css";
import 'react-toastify/dist/ReactToastify.css';

export default function App() {
    return (
        <>
            <header className="bg-gray-900 text-white flex items-center h-12 w-full">
                <div className="container mx-auto">
                    <a className="navbar-brand" href="/">
                        Timelogger
                    </a>
                </div>
            </header>

            <main>
                <div className="container mx-auto">
                    <Routes>
                        <Route path="/" element={<Projects />} />
                        <Route path="/:id" element={<Project />} />
                    </Routes>
                </div>
            </main>

            <ToastContainer />
        </>
    );
}
