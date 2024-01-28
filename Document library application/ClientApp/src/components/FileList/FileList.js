import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDownload, faTrash, faEye } from '@fortawesome/free-solid-svg-icons';
import ShareDropdown from '../ShareDropdown/ShareDropdown';
import DateTimeFormatter from '../DateTimeFormatter/DateTimeFormatter';
import './FileList.css';
import getFileIcon from '../FileIcon/FileIcon';
import PreviewModal from '../PreviewModal/PreviewModal';

const FileList = ({ files, handleDelete, handleDownload, handleShare }) => {
    const [sortOrder, setSortOrder] = useState('asc');
    const [previewFile, setPreviewFile] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

    if (files[0].message) {
        return <p>No Data Found</p>;
    }

    const sortedFiles = [...files].sort((a, b) => {
        if (sortOrder === 'asc') {
            return new Date(a.createTime) - new Date(b.createTime);
        } else {
            return new Date(b.createTime) - new Date(a.createTime);
        }
    });

    const toggleSortOrder = () => {
        setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
    };

    const handlePreview = (file) => {
        setPreviewFile(file.filePath);
        setIsModalOpen(true);
    };

    return (
        <div>
            <br />
            <h3>List of all uploaded files</h3>
            <table className="file-table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th onClick={toggleSortOrder}>
                            Created Date
                            {sortOrder === 'asc' ? <span>&uarr;</span> : <span>&darr;</span>}
                        </th>
                        <th>Delete</th>
                        <th>Download</th>
                        <th>No. of Downloads</th>
                        <th>Preview</th>
                        <th>Share</th>
                    </tr>
                </thead>
                <tbody>
                    {sortedFiles.map((file, index) => (
                        <tr key={index}>
                            <td className="file-info">
                                <div className="file-icon">{getFileIcon(file.fileName)}</div>
                                <div className="file-name">{file.fileName}</div>
                            </td>
                            <td><DateTimeFormatter createTime={file.createTime} /></td>
                            <td>
                                <button
                                    className="delete-button small-button"
                                    onClick={() => handleDelete(file.id)}
                                >
                                    <FontAwesomeIcon icon={faTrash} />
                                </button>
                            </td>
                            <td>
                                <button
                                    className="download-button small-button"
                                    onClick={() => handleDownload(file)}
                                >
                                    <FontAwesomeIcon icon={faDownload} />
                                    {/*{file.downloadCount > 0 && (*/}
                                    {/*    <span className="download-badge">{file.downloadCount}</span>*/}
                                    {/*)}*/}
                                </button>
                            </td>
                            <td>{file.downloadCount}</td>
                            <td>
                                <button className="preview-button" onClick={() => handlePreview(file)}>
                                    <FontAwesomeIcon icon={faEye} />
                                </button>
                            </td>
                            <td>
                                <ShareDropdown attachmentId={file.id} handleShare={handleShare} />
                            </td>
                        </tr>
                    ))}
                </tbody>

            </table>

            {isModalOpen && <PreviewModal fileUrl={previewFile} closeModal={() => setIsModalOpen(false)} />}

        </div>
    );
};

export default FileList;
