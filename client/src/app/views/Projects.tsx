import React, { useState, useEffect } from "react";
import ProductsTable from "../components/ProductsTable";
import NewEntryModal from "../components/NewEntryModal";
import { Project } from "../Types";
import { getAll } from "../api/projects";

interface SearchProps {
    items: [],
    count: number
}

export default function Projects() {
    const [showModal, setShowModal] = useState(false);
    const [searchText, setSearchText] = useState('');
    const [projects, setProjects] = useState(Array<Project>());

    const getProjects = () => {
        getAll(searchText)
            .then(({ items }: SearchProps) => {
                setProjects(items);
            })
            .catch(error => console.log(error));
    }

    useEffect(() => {
        getProjects();
    }, [])

    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                        onClick={() => {
                            setShowModal(true);
                        }}>
                        Add entry
                    </button>
                    {showModal && <NewEntryModal projects={projects} setOpenModal={setShowModal} onSuccess={() => {
                        setShowModal(false);
                        getProjects();
                    }} />}
                </div>

                <div className="w-1/2 flex justify-end">
                    <form>
                        <input
                            className="border rounded-full py-2 px-4"
                            type="search"
                            placeholder="Search"
                            aria-label="Search"
                            value={searchText}
                            onChange={e => {
                                setSearchText(e.target.value)
                            }}
                        />
                        <button
                            className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                            type="button"
                            onClick={getProjects}
                        >
                            Search
                        </button>
                    </form>
                </div>
            </div>

            {projects && <ProductsTable projects={projects} />}
        </>
    );
}
