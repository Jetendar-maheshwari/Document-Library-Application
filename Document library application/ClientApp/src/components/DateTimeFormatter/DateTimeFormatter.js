import React from 'react';
import PropTypes from 'prop-types';

const DateTimeFormatter = ({ createTime }) => {
    const formatCreateTime = (unixTimestamp) => {
        const createTime = new Date(unixTimestamp * 1000);
        const day = String(createTime.getDate()).padStart(2, '0');
        const month = String(createTime.getMonth() + 1).padStart(2, '0');
        const year = createTime.getFullYear();
        const hours = String(createTime.getHours()).padStart(2, '0');
        const minutes = String(createTime.getMinutes()).padStart(2, '0');
        const seconds = String(createTime.getSeconds()).padStart(2, '0');
        return `${day}.${month}.${year} ${hours}:${minutes}:${seconds}`;
    };

    return <span>{formatCreateTime(createTime)}</span>;
};

DateTimeFormatter.propTypes = {
    createTime: PropTypes.number.isRequired
};

export default DateTimeFormatter;


