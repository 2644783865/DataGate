<template>
  <el-row class="warp">
    <el-col :span="24">
      <h1>欢迎使用{{appConfig.systemName}}!</h1>
    </el-col>
  </el-row>
</template>
<style>
.time {
  font-size: 13px;
  color: #999;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
}

.image {
  width: 100%;
  display: block;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}

.clearfix:after {
  clear: both;
}

.chart-container {
  width: 100%;
}
.chart-container .el-col {
  padding: 30px 20px;
}
</style>

<script>
export default {};
</script>
