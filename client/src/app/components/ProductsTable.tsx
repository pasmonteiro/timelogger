import React from "react";
import { Project } from "../Types";
import { useTable, useSortBy } from "react-table";
import { useNavigate } from 'react-router-dom';

const columnsDefinition = [
    {
        Header: 'Projects',
        columns: [
            {
                Header: 'Id',
                accessor: 'id',
                id: 'id'
            },
            {
                Header: 'Name',
                accessor: 'name',
                id: 'name',
                disableSortBy: true
            },
            {
                Header: 'Status',
                accessor: 'status',
                id: 'status',
                disableSortBy: true
            },
            {
                Header: 'Total Minutes',
                accessor: 'totalMinutes',
                id: 'totalMinutes',
                disableSortBy: true
            },
            {
                Header: 'Deadline',
                accessor: 'deadline',
                id: 'deadline'
            }
        ]
    }
];

interface Props {
    projects: Array<Project>
}

const ProductsTable = ({ projects }: Props) => {
    const navigate = useNavigate();
    const columns = React.useMemo(() => columnsDefinition, []);

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow
    } = useTable({
        columns,
        data: projects,
    }, useSortBy);

    return (
        <>
            <table className="table-fixed w-full" {...getTableProps()}>
                <thead className="bg-gray-200">
                    {headerGroups.map((headerGroup) => (
                        <tr {...headerGroup.getHeaderGroupProps()}>
                            {headerGroup.headers.map((column: any) => (
                                <th className="border px-4 py-2 w-12" {...column.getHeaderProps(column.getSortByToggleProps())}>
                                    {column.render("Header")}
                                    <span>
                                        {column.isSorted
                                            ? column.isSortedDesc
                                                ? ' 🔽'
                                                : ' 🔼'
                                            : ''}
                                    </span>
                                </th>
                            ))}
                        </tr>
                    ))}
                </thead>
                <tbody {...getTableBodyProps()}>
                    {rows.map((row) => {
                        prepareRow(row);
                        return (
                            <tr {...row.getRowProps()} onClick={() => {
                                navigate(`/${row.original.id}`);
                            }}>
                                {row.cells.map((cell) => {
                                    return <td className="border px-4 py-2" {...cell.getCellProps()}>{cell.render("Cell")}</td>;
                                })}
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </>)
}

export default ProductsTable;
