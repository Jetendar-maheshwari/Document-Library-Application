import React, { useState, useEffect } from "react";
import "./DocumentUpload.css";
import FileList from "../FileList/FileList";

const DocumentUpload = () => {
  const [uploadedFiles, setUploadedFiles] = useState([]);
  const [tempFiles, setTempFiles] = useState([]);
  const [showFileList, setShowFileList] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchData();
  }, []);

  // This function is used to get the all attachment for database
  const fetchData = async () => {
    try {
      const response = await fetch("attachment/get");
      if (response.ok) {
        const data = await response.json();
        if (data.length > 0 && !("Message" in data[0])) {
          setUploadedFiles(data);
          setShowFileList(true);
        } else {
          console.log("No attachment found.");
        }
      } else {
        console.error("Failed to fetch data");
      }
    } catch (error) {
      console.error("Error fetching data:", error);
    } finally {
      setLoading(false);
    }
  };

  // This function is used to upload in the directory and also add the save the data in database
  const handleUploadButtonClick = async (file) => {
    const formData = new FormData();
    formData.append("file", file.file);

    try {
      const response = await fetch("attachment/add", {
        method: "POST",
        body: formData,
      });

      if (response.ok) {
        const data = await response.json();
        console.log("File uploaded successfully!", data);
        fetchData();
        const updatedFiles = tempFiles.filter((f) => f !== file);
        setTempFiles(updatedFiles);
        const updatedUploadedFiles = [
          ...uploadedFiles,
          { ...file, uploadedDate: new Date(), id: data.id },
        ];
        setUploadedFiles(updatedUploadedFiles);
      } else {
        console.error("Failed to upload file");
      }
    } catch (error) {
      console.error("Error uploading file:", error);
    }
  };

  // This function is used to delete the attachmend from the database table
  const handleDelete = async (id) => {
    try {
      const response = await fetch(`attachment/delete/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (response.ok) {
        const updatedFiles = uploadedFiles.filter((file) => file.id !== id);
        setUploadedFiles(updatedFiles);
      } else {
        console.error("Failed to delete file");
      }
    } catch (error) {
      console.error("Error deleting file:", error);
    }
  };


  const handleDownload = async (file) => {
    try {
      const response = await fetch(`attachment/download/${file.fileName}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (response.ok) {
        const updatedFiles = [...uploadedFiles];
        const index = updatedFiles.findIndex((f) => f.id === file.id);
        if (index !== -1) {
          updatedFiles[index].downloadCount += 1;
          setUploadedFiles(updatedFiles);
        }
        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", file.fileName);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        console.log("File downloaded successfully");
      } else {
        console.error("Failed to download file");
      }
    } catch (error) {
      console.error("Error downloading file:", error);
    }
  };

  const handleShare = (selectedOption) => {
    console.log("Sharing with time period:", selectedOption);
  };

    const handleFileSelection = (fileList) => {
        const acceptedFileTypes = [
            ".pdf",
            ".xls",
            ".xlsx",
            ".doc",
            ".docx",
            ".txt",
            ".jpg",
            ".jpeg",
            ".png",
        ];

        const newFiles = [];
        for (let i = 0; i < fileList.length; i++) {
            const file = fileList[i];
            const fileExtension = "." + file.name.split(".").pop().toLowerCase();
            if (acceptedFileTypes.includes(fileExtension)) {
                newFiles.push({
                    name: file.name,
                    file: file,
                    uploadedDate: new Date(),
                });
            }
        }

        return newFiles;
    };

    const handleDrop = (event) => {
        event.preventDefault();
        const fileList = event.dataTransfer.files;
        const newFiles = handleFileSelection(fileList);
        setTempFiles(newFiles);
    };

    const handleFileInputChange = (event) => {
        const fileList = event.target.files;
        const newFiles = handleFileSelection(fileList);
        setTempFiles(newFiles);
    };

    const handleDragOver = (e) => {
        e.preventDefault();
    };

  const handleDeleteButtonClick = (file) => {
    const updatedFiles = tempFiles.filter((f) => f !== file);
    setTempFiles(updatedFiles);
  };

  return (
    <div>
          <div
              className="dropzone-container"
              onDragOver={handleDragOver}
              onDrop={handleDrop}
          >
              <label htmlFor="fileInput" className="dropzone-label">
                  Drag and drop document files here, or click to select them
              </label>
              <input
                  id="fileInput"
                  type="file"
                  accept=".pdf,.xls,.xlsx,.doc,.docx,.txt,.jpg,.jpeg,.png"
                  onChange={handleFileInputChange}
                  multiple
              />
          </div>

          <p>Please Upload file less then 10 MB for testing</p>

        {tempFiles.length > 0 && (
          <div>
            <br />
            <p>
              Select upload button to save the file. If you don't want to upload
              it click delete button to remove
            </p>
            {tempFiles.map((file, index) => (
              <div key={index} className="file-item">
                <span>{file.name}</span>
                <button
                  className="upload-button small-button"
                  onClick={() => handleUploadButtonClick(file)}
                >
                  Upload
                </button>
                <button
                  className="delete-button small-button"
                  onClick={() => handleDeleteButtonClick(file)}
                >
                  Delete
                </button>
              </div>
            ))}
          </div>
        )}

        {uploadedFiles.length > 0 && showFileList && !loading && (
          <div>
            <FileList
              files={uploadedFiles}
              handleDelete={handleDelete}
              handleDownload={handleDownload}
              handleShare={handleShare}
            />
          </div>
        )}
        {loading && <p>Loading...</p>}
      </div>
  );
};

export default DocumentUpload;
