const fs = require('fs');
const o = {
  s1_sub: 'S\u1ea3n ph\u1ea9m',
  done: true
};
fs.writeFileSync('C:/Users/Admin/Downloads/FUge/web/output/tx-test.json', JSON.stringify(o));
console.log('OK:', o.s1_sub);