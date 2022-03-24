# GUI Performance Tests

**CPU is devided by core**
_So 100% CPU = 1 Core full usage, on a 6 core system = 16.66%, on an 8 Core = 12.5%_


## Desktop Tests

### AvaloniaTest

Sample                  | Chart         | Delay | Tasks | CPU   | Memory    | Notes
------------------------|---------------|-------|-------|-------|-----------|--------------------------------------
Desktop                 | Candlestick   | 1000  | 1     | 50%   | 74MB      | Update same bar for 60 seconds, then draw new line
Desktop                 | Candlestick   | 100   | 10    | 56%   | 73.4MB    | Update same bar for 60 seconds, then draw new line
Desktop                 | Candlestick   | 100   | 10    | 60%   | 74.7MB    | Continually draw new bars
Desktop                 | Candlestick   | 100   | 10    | 57%   | 74.7MB    | Continually draw new bars ; default Easing & `AnimationSpeed`

**Note**
- Window size doesn't seem to consume a lot more resources.
- package
  ```xml
    <PackageReference Include="LiveChartsCore.SkiaSharpView.Avalonia" Version="2.0.0-beta.101" />
    <PackageReference Include="Avalonia" Version="0.10.999-cibuild0019370-beta" />
  ```




### LiveCharts2/samples

Sample                  | Chart         | Delay | Tasks | CPU   | Memory
------------------------|---------------|-------|-------|-------|-------------
General/MultiThreading2 | Line          | 100   | 10    | 50%   | 74MB

- version: github March 23 2022


## Web Tests

### AvaloniaTest

Sample                  | Chart         | Delay | Tasks | CPU   | Memory    | Notes
------------------------|---------------|-------|-------|-------|-----------|--------------------------------------
Desktop                 | Candlestick   | 100   | 10    | 94%   | 314MB    | Continually draw new bars ; default Easing & `AnimationSpeed`
Desktop                 | Candlestick   | 1000  | 10    | 77%   | 284MB    | Update same bar for 60 seconds, then draw new line ; default Easing & `AnimationSpeed`

~~**The memory access error was caused by UPDATING THE LAST BAR**~~
```cs
_values[^1].High = _values[^1].High + 1;
```


**The memory access error was caused by MOUSING OVER**

```
051db546:0x297b47 
        
       Uncaught RuntimeError: memory access out of bounds
    at SkSL::ProgramUsage::isDead(SkSL::Variable const&) const (051db546:0x297b47)
    at SkSL::Compiler::optimize(SkSL::Program&) (051db546:0x2f997e)
    at SkSL::Compiler::convertProgram(SkSL::Program::Kind, SkSL::String, SkSL::Program::Settings const&, std::__2::vector<std::__2::unique_ptr<SkSL::ExternalValue, std::__2::default_delete<SkSL::ExternalValue> >, std::__2::allocator<std::__2::unique_ptr<SkSL::ExternalValue, std::__2::default_delete<SkSL::ExternalValue> > > > const*) (051db546:0x2f948c)
    at GrSkSLtoGLSL(GrGLContext const&, SkSL::Program::Kind, SkSL::String const&, SkSL::Program::Settings const&, SkSL::String*, GrContextOptions::ShaderErrorHandler*) (051db546:0x305143)
    at GrGLProgramBuilder::finalize(GrGLPrecompiledProgram const*) (051db546:0x305e71)
    at GrGLProgramBuilder::CreateProgram(GrGLGpu*, GrRenderTarget*, GrProgramDesc const&, GrProgramInfo const&, GrGLPrecompiledProgram const*) (051db546:0x3057de)
    at GrGLGpu::ProgramCache::findOrCreateProgram(GrRenderTarget*, GrProgramDesc const&, GrProgramInfo const&, GrGpu::Stats::ProgramCacheResult*) (051db546:0x30943a)
    at GrGLGpu::ProgramCache::findOrCreateProgram(GrRenderTarget*, GrProgramInfo const&) (051db546:0x309270)
    at GrGLGpu::flushGLState(GrRenderTarget*, GrProgramInfo const&) (051db546:0x314eb1)
    at GrGLOpsRenderPass::onBindPipeline(GrProgramInfo const&, SkRect const&) (051db546:0x30cdf0)
```

See <https://www.bing.com/search?q=Uncaught%20RuntimeError>

**confirmed -- THIS ONLY HAPPENS WHEN THE DEVELOPER CONSOLE / TOOLS IS OPEN**

