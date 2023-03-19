import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { get } from "../api/projects";
import { Project, Entry} from "../Types";
import EntriesTable from "../components/EntriesTable";

interface SearchProps {
    project: Project,
    entries: Entry[],
    success: boolean
}

export default function ProjectView() {
    const [project, setProject] = useState<Project>();
    const [entries, setEntries] = useState(Array<Entry>());
    const params= useParams()

    const getProjectDetails = () => {
        get(Number(params.id))
            .then(({ entries, project }: SearchProps) => {
                setEntries(entries);
                setProject(project);
            })
            .catch(error => console.log(error));
    }

    useEffect(() => {
        getProjectDetails();
    }, [])

    return (
        <>
            <p>{project?.name}</p>
            {entries && <EntriesTable entries={entries} />}
        </>
    );
}
