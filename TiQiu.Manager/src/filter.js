angular.module('tiqiu')
    .filter('orderType', function() {
        return function(type) {
            type = type + '';
            switch (type) {
                case '0':
                    return '普通预定';
                case '1':
                    return 'PK';
                case '2':
                    return '散打';
                default:
                    return '其他';
            }
        };
    })
    .filter('orderStatus', function() {
        return function(status) {
            status = status + '';
            return {
                '0': '可预定',
                '1': '预定中',
                '2': '预定成功',
                '3': '预定确认',
                '10': '签到成功',
                '20': '结束',
                '50': '过期',
                '60': '取消',
                '-1': '不可用'
            }[status];
        };
    })
    .filter('orderDate', function() {
        return function(order) {
            return moment(order.OrderDate).format('MM月DD日 ') + order.Start.slice(0, 5) + ' 到 ' + order.End.slice(0, 5);
        };
    })
    .filter('field', function() {
        return function(order) {
            return order.FieldName + ' - ' + order.FieldItemName;
        };
    });