import React from 'react';
import {
    FaFilePdf,
    FaFileWord,
    FaFileImage,
    FaFile,
    FaFileExcel,
    FaFilePowerpoint,
    FaFileAlt,
} from 'react-icons/fa';

const getFileIcon = (fileName) => {
    if (typeof fileName !== 'string' || !fileName) {
        return <FaFile />;
    }

    const extension = fileName.split('.').pop().toLowerCase();

    // If we want we can add more here for others extensions
    switch (extension) {
        case 'pdf':
            return <FaFilePdf color="red" />;
        case 'doc':
        case 'docx':
            return <FaFileWord color="blue" />;
        case 'jpg':
        case 'jpeg':
        case 'png':
        case 'gif':
            return <FaFileImage color="green" />;
        case 'xls':
        case 'xlsx':
            return <FaFileExcel color="green" />;
        case 'ppt':
        case 'pptx':
            return <FaFilePowerpoint color="orange" />;
        case 'txt':
            return <FaFileAlt color="gray" />;
        default:
            return <FaFileAlt color="gray" />;
    }
};

export default getFileIcon;
